import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../core/_guards/auth.guard';

import { SettingsComponent } from './settings/settings.component';
import { SettingsWrapperComponent } from './settings-wrapper/settings-wrapper.component';
import { PermissionsListComponent } from './permissions-list/permissions-list.component';
import { PermissionsComponent } from './permissions/permissions.component';

const routes: Routes = [{
  path: '', component: SettingsWrapperComponent,
  canActivate: [AuthGuard], canActivateChild: [AuthGuard],
  children: [
    { path: '', component: SettingsComponent, data: {'breadcrumb': ['settings']} },
    { path: 'permissions', component: SettingsWrapperComponent,
      children: [
        {
          path: '',
          component: PermissionsListComponent,
          data: {
            permissionCode: ['ViewPermissions'],
            'breadcrumb': ['settings', 'permissions']
          }
        },
        { path: 'add', component: PermissionsComponent,
          data: {'breadcrumb': ['settings', 'permissions', 'addPermission']}
        },
        {
          path: 'edit/:permissionId',
          component: PermissionsComponent,
          data: {
            'permissionCode': ['EditPermissions'],
            'breadcrumb': ['settings', 'permissions', 'editPermission']
          }
        },
      ]
    },
    { path: '**', redirectTo: 'settings', pathMatch: 'full' },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule { }
