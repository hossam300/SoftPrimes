import { LoaderService } from './../loader.service';
import {Injectable} from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor, HttpErrorResponse
} from '@angular/common/http';
import {Observable, of, throwError} from 'rxjs/index';
import {catchError, delay, finalize, map, mergeMap, retryWhen, take} from 'rxjs/internal/operators';
import {TokenStoreService} from '../token-store.service';
import {Router} from '@angular/router';
import {AuthTokenType} from '../../_models/auth-token-type';
import { LayoutService } from '../layout.service';

@Injectable()
export class InterceptorService implements HttpInterceptor {
  private delayBetweenRetriesMs = 1000;
  private numberOfRetries = 3;
  private authorizationHeader = 'Authorization';
  private requests: HttpRequest<any>[] = [];
  private error = false;

  constructor(
    private tokenStoreService: TokenStoreService,
    private router: Router,
    private loader: LoaderService,
    private layout: LayoutService
  ) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.error = false;
    const accessToken = this.tokenStoreService.getRawAuthToken(AuthTokenType.AccessToken);
    this.loader.addLoader();
    request = request.clone({withCredentials: false});
    if (accessToken) {
      request = request.clone({
        headers: request.headers.set(this.authorizationHeader, `Bearer ${accessToken}`).set('Cache-Control', 'no-cache')
        .set('Pragma', 'no-cache')
      });
      this.requests.push(request);
      return next.handle(request).pipe(
        retryWhen(errors => errors.pipe(
          mergeMap((error: HttpErrorResponse, retryAttempt: number) => {
            this.error = true;
            // if in Case UnAuthorize
            if (error.status === 401 || error.status === 403 ) {
              const newRequest = this.getNewAuthRequest(request);
              if (newRequest === null) {
                this.tokenStoreService.deleteAuthTokens();
                // remove annotation cardentials
                this.loader.removeLoader();
                return this.router.navigate(['/login']);
              }
            }
            if (retryAttempt === this.numberOfRetries - 1) {
              // console.log(
              //  `%cHTTP call '${request.method}' ${request.url} failed after ${this.numberOfRetries} retries.`,
              //  `background: #a55656; color: #ffeded; font-weight: bold; padding: 2px 5px;`
              // );
              this.loader.removeLoader();
              return throwError(error); // no retry
            }

            switch (error.status) {
              case 400:
              case 404:
              case 500:
                this.loader.removeLoader();
                return throwError(error); // no retry
            }
            this.loader.removeLoader();
            return of(error); // retry
          }),
          delay(this.delayBetweenRetriesMs),
          take(this.numberOfRetries)
        )),
        catchError((error: any, caught: Observable<HttpEvent<any>>) => {
          console.error({error, caught});
          if (error.status === 401 || error.status === 403) {
            const newRequest = this.getNewAuthRequest(request);
            if (newRequest) {
              this.loader.removeLoader();
              return next.handle(newRequest);
            }
            // remove annotation cardentials
            this.loader.removeLoader();
            this.router.navigate(['/login']);
          }
          this.loader.removeLoader();
          this.layout.handleFailMsg(this.requests[0]);
          return throwError(error);
          // return of(error);
        }),
        finalize(() => {
          if (!this.error) {
            this.layout.handleSuccessMsg(this.requests[0]);
          }
          this.requests = this.requests.filter(x => x !== request);
          if (!this.requests.length) {
            this.loader.removeLoader();
          }
        })
      );
    } else {
      // login page
      this.loader.removeLoader();
      return next.handle(request).pipe(finalize(() => {
        this.loader.removeLoader();
      }));
    }
  }

  getNewAuthRequest(request: HttpRequest<any>): HttpRequest<any> | null {
    const newStoredToken = this.tokenStoreService.getRawAuthToken(AuthTokenType.AccessToken);
    const requestAccessTokenHeader = request.headers.get(this.authorizationHeader);
    if (!newStoredToken || !requestAccessTokenHeader) {
      return null;
    }
    const newAccessTokenHeader = `Bearer ${newStoredToken}`;
    if (requestAccessTokenHeader === newAccessTokenHeader) {
      return null;
    }
    return request.clone({headers: request.headers.set(this.authorizationHeader, newAccessTokenHeader), withCredentials: false});
  }

}
