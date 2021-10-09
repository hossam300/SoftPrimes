import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { concat, Observable, of, Subject, Subscription } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';
import { LoaderService } from 'src/app/core/_services/loader.service';
import { PermissionDTO, RoleDetailsDTO, RoleDTO } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { SettingsCrudsService } from '../settings-cruds.service';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {
  roles: RoleDetailsDTO;
  routerSubscription: Subscription;
  createMode: boolean;
  controller = 'Roles';
  permissionIds = [];
  permissions$: Observable<PermissionDTO[]>;
  permissionsInput$ = new Subject<string>();
  permissionsLoading = false;

  constructor(
    private settingsCrud: SettingsCrudsService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
  }

  ngOnInit() {
    // get lookups
    this.getPermissions();

    this.roles = new RoleDetailsDTO();
    this.routerSubscription = this.route.params.subscribe(r => {
      if (!r.rolesId) {
        this.createMode = true;
        this.roles.permissions = [];
      } else {
        this.createMode = false;
        this.settingsCrud.getDTOById(this.controller, +r.rolesId).subscribe(roles => {
          const permissions = roles.permissions.map(y => y.permissionId);
          this.roles = new RoleDetailsDTO({
            id: roles.id,
            roleNameAr: roles.roleNameAr,
            roleNameEn: roles.roleNameEn,
            permissions: permissions
          });
        });
      }
    });
  }

  updateRoles() {
    // this.loader.addLoader();
    this.settingsCrud.updateRoles(this.roles).subscribe(result => {
      // this.loader.removeLoader();;
      if (result) {
        this.router.navigate(['/settings/roles']);
      }
    });
  }

  insertRoles() {
    // this.loader.addLoader();
    this.settingsCrud.insertRoles(this.roles).subscribe(result => {
      // this.loader.removeLoader();;
      if (result) {
        this.router.navigate(['/settings/roles']);
      }
    });
  }

  getPermissions() {
    this.settingsCrud.getPermissionsLookup().subscribe(value => {
      this.permissions$ = concat(
        of(value), // default items
        this.permissionsInput$.pipe(
          debounceTime(200),
          distinctUntilChanged(),
          tap(() => this.permissionsLoading = true),
          switchMap(term => this.settingsCrud.getPermissionsLookup(term).pipe(
            catchError(() => of([])), // empty list on error
            tap(() => this.permissionsLoading = false)
          ))
        )
      );
    });
  }

}
