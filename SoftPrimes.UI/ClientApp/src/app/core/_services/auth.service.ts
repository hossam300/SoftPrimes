import { Injectable } from '@angular/core';
import { AccessToken, AuthTicketDTO, SwaggerClient, UserLoginModel } from './swagger/SwaggerClient.service';
import { BehaviorSubject, Observable, throwError } from 'rxjs/index';
import { Router } from '@angular/router';
import { TokenStoreService } from './token-store.service';
import { RefreshTokenService } from './refresh-token.service';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthTokenType } from '../_models/auth-token-type';
import { catchError, finalize, map } from 'rxjs/internal/operators';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { LocalizationService } from './localization.service';
import { StoreService } from './store.service';


@Injectable()
export class AuthService {
  private user = new BehaviorSubject<AuthTicketDTO>(null);
  userIsLocked = new BehaviorSubject<boolean>(false);
  currentUser = this.user.asObservable();

  private authStatusSource = new BehaviorSubject<boolean>(false);
  authStatus$ = this.authStatusSource.asObservable();

  private isEmployee = new BehaviorSubject<boolean>(false);
  isEmployee$ = this.isEmployee.asObservable();

  accountSwitched: string;
  isArabic = false;
  isAdmin = false;

  numberOfWrongRitriesToSendNotifcation = 0;
  numberOfWrongRitriesToLockAccount = 0;
  numberOfWrongLoginRitries = 0;
  lastLoginTryUserName = '';

  constructor(
    private swagger: SwaggerClient,
    private router: Router,
    private tokenStoreService: TokenStoreService,
    private refreshTokenService: RefreshTokenService,
    private toastr: ToastrService,
    private translate: TranslateService,
    private store: StoreService,
    private localization: LocalizationService
  ) {
    this.updateStatusOnPageRefresh();
    this.refreshTokenService.scheduleRefreshToken(this.isAuthUserLoggedIn());
    this.translate.onLangChange.subscribe(_ => {
      this.translate.get('AccountSwitched').subscribe(res => this.accountSwitched = res);
    });
    const _user = localStorage.getItem('user');
    const _isEmployee = localStorage.getItem('isEmployee');
    if (_user) {
      this.setUser(JSON.parse(_user), _isEmployee === 'true');
    }
    this.localization.isArabic$.subscribe(value => {
      this.isArabic = value;
    });
  }

  setUser(user: AuthTicketDTO, isEmployee = false, fromTemp = true) {
    if (fromTemp) {
      if (this.user.value !== null && !this.router.url.includes('/vip')) {
        this.router.navigate(['/']);
      }
    }

    this.user.next(user);
    if (user) {
      this.isEmployee.next(isEmployee);
      if (!isEmployee) {
        document.body.classList.remove('employee');
        this.translate.get('On').subscribe(translateValue => {

          if (this.isArabic) {
            this.toastr.info(`${user.fullNameAr} ${translateValue}`, this.accountSwitched);
          } else {
            this.toastr.info(`${user.fullNameEn} ${translateValue}`, this.accountSwitched);
          }
        });
      } else {
        document.body.classList.add('employee');
      }
      localStorage.setItem('user', JSON.stringify(user));
      localStorage.setItem('isEmployee', isEmployee.toString());
      this.store.loggedUser$.next(user); // share user in all system - if changed the user or if changed the organization
    }
  }

  setEmployeeStatus(bool: boolean) {
    this.isEmployee.next(bool);
  }

  login(credentials: UserLoginModel, culture: string): Observable<{}> {
    return this.swagger.apiAccountLoginPost(culture, credentials)
      .pipe(
        map((response: AccessToken) => {
          this.isAdmin = response.isAdmin;
          if (response.isAdmin) { localStorage.setItem('isAdmin', JSON.stringify(response.isAdmin)); }
          if (!response.access_token) {
            this.authStatusSource.next(false);
            if (response['isLocked']) { this.userIsLocked.next(true); }
            if (!response.isTemp) {
              // first wrong trial
              if (!this.lastLoginTryUserName && !this.numberOfWrongLoginRitries) {
                this.lastLoginTryUserName = credentials.username;
                this.numberOfWrongLoginRitries += 1;
              } // another wrong login for the same username
              // tslint:disable-next-line:one-line
              else if (this.lastLoginTryUserName === credentials.username) {
                this.numberOfWrongLoginRitries += 1;
              } else { // wrong login for different username
                this.lastLoginTryUserName = credentials.username;
                this.numberOfWrongLoginRitries = 1;
              }
            }

            return false;
          } else {
            // set credentials to session storage.
            let Encryptedcredentials;
            Encryptedcredentials = {
              '_1_': response['userTN'],
              '_2_': response['userTP'],
              '_3_': true
            };
            if (response['userTN']) { sessionStorage.setItem('utn_utp', JSON.stringify(Encryptedcredentials)); }
            this.tokenStoreService.storeLoginSession(response);
            this.refreshTokenService.scheduleRefreshToken(true);
            this.authStatusSource.next(true);
            // set cookies
            // const cookies = JSON.parse(sessionStorage['utn_utp'])['_1_'];
            // this.cookie.set('&utn', cookies);
            // update backend with local culture
            const currentLang = localStorage.getItem('culture');
            this.numberOfWrongLoginRitries = 0;
            return true;
          }
        }),
        catchError((error: HttpErrorResponse) => throwError(error))
      );
  }

  logout(): void {
    const refreshToken = encodeURIComponent(this.tokenStoreService.getRawAuthToken(AuthTokenType.RefreshToken));
    this.swagger.apiAccountLogoutGet(refreshToken)
      .pipe(
        map(response => response || {}),
        catchError((error: HttpErrorResponse) => throwError(error)),
        finalize(() => {
          this.setUser(null);
          this.tokenStoreService.deleteAuthTokens();
          this.refreshTokenService.unscheduleRefreshToken(true);
          this.authStatusSource.next(false);
          // remove annotation cardentials
          sessionStorage.removeItem('_ap');
          localStorage.removeItem('pageSize'); // remove pageSize on logout
        }))
      .subscribe();
  }

  isAuthUserLoggedIn(): boolean {
    return this.tokenStoreService.hasStoredAccessAndRefreshTokens() &&
      !this.tokenStoreService.isAccessTokenTokenExpired();
  }

  isAuthUserHasPermissions(requiredPermissions: string[]): boolean {
    const user = this.user.value;
    // if (!user || !user.permissions) {
    //   return false;
    // }

    // return requiredPermissions.some(requiredPermission => {
    //   if (user.permissions) {
    //     return user.permissions.indexOf(requiredPermission.toUpperCase()) >= 0;
    //   } else {
    //     return false;
    //   }
    // });
    return true;
  }

  private updateStatusOnPageRefresh(): void {
    this.authStatusSource.next(this.isAuthUserLoggedIn());
  }

  updateUserLockState(userName): void {
    let userState = 0;  // 1 lock;  2 Notify user;
    let changeUserState = false;
    // send notificatuion by mail
    if (this.numberOfWrongLoginRitries === this.numberOfWrongRitriesToSendNotifcation) {
      userState = 2;
      changeUserState = true;
    }
    // lock user
    if (this.numberOfWrongLoginRitries === this.numberOfWrongRitriesToLockAccount) {
      userState = 1;
      changeUserState = true;
    }
  }
}
