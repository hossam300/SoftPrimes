import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { concat, Observable, of, Subject, Subscription } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';
import { PermissionDTO, RoleDTO } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { SettingsCrudsService } from '../settings-cruds.service';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {
  roles: RoleDTO;
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
    private router: Router
  ) {
  }

  ngOnInit() {
    this.routerSubscription = this.route.params.subscribe(r => {
      if (!r.rolesId) {
        this.createMode = true;
        this.roles = new RoleDTO();

      } else {
        this.createMode = false;
        this.settingsCrud.getDTOById(this.controller, +r.rolesId).subscribe(Roles => {
          this.roles = Roles;
        });
      }
    });
    this.getPermissions();
  }

  updateRoles() {
    this.settingsCrud.updateDTO(this.controller, [this.roles]).subscribe(result => {
      if (result) {
        this.router.navigate(['/settings/Roles']);
      }
    });
  }

  insertRoles() {
    this.settingsCrud.insertDTO(this.controller, [this.roles]).subscribe(result => {
      if (result) {
        this.router.navigate(['/settings/Roles']);
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

  selectPermission(event) {
    console.log(event, 'permissions changed');
    const permissionArr = this.permissionIds.map(id => {
      return {
        permissionId: id
      };
    });
    console.log(this.permissionIds, 'permissions arr');
  }

}
