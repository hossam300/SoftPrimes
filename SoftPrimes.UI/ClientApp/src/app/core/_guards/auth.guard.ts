import { Injectable, Renderer2 } from '@angular/core';
import { CanActivate, CanActivateChild, Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AuthTicketDTO } from '../_services/swagger/SwaggerClient.service';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LayoutService } from '../_services/layout.service';
import { TranslateService } from '@ngx-translate/core';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild {
  user: AuthTicketDTO;
  allowedPermissionCode: string;
  firstURL = true;
  tempUrl: string;
  ssokey: string;

  constructor(private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private auth: AuthService,
    private toastr: ToastrService,
    private translate: TranslateService,
    private layoutService: LayoutService) {
    this.auth.currentUser.subscribe(user => {
      this.user = user;
      // && user.permissions.indexOf(this.allowedPermissionCode) === -1
      if (this.allowedPermissionCode && user) {
        this.translate.get('PermissionTaken').subscribe(x => this.toastr.error(this.allowedPermissionCode, x));
        // this.toastr.error(this.allowedPermissionCode, 'Permission Taken');
        this.router.navigate(['/']);
      }
    });
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.user && this.authService.isAuthUserLoggedIn()) {
      return true;
    } else {
      // not logged in so redirect to login page with the return url and return false
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      return false;
    }
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const returnUrl = state.url;
    if (!this.authService.isAuthUserLoggedIn()) {
      this.router.navigate(['/login'], { queryParams: { returnUrl } });
      return false;
    }
    if (route.data.permissionCode) {
      // get permissionCode array and convert to uppercase ...
      const permissionCode = route.data.permissionCode.map(function(x: string) { return x.toUpperCase(); });
      let allow = false;
      // && this.user.permissions.some((perCode) => permissionCode.indexOf(perCode.toUpperCase()) > -1)
      if (this.user) {
        this.allowedPermissionCode = permissionCode;
        this.firstURL = false;
        allow = true;
      } else {
        this.translate.get('ActionNotAllowed').subscribe(x => this.toastr.error(permissionCode, x));
        // this.toastr.error(permissionCode, 'Action Not Allowed');
        // TODO: navigation history
        if (this.firstURL) {
          this.router.navigate(['/']);
        }
      }
      return allow;
    } else {
      this.allowedPermissionCode = null;
      return true;
    }
  }
}
