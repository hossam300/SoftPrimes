import { LocalizationComponent } from './localization/localization.component';
import { LocalizationListComponent } from './localization-list/localization-list.component';
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
          data: {'breadcrumb': ['settings', 'permissions', 'add']}
        },
        {
          path: 'edit/:permissionId',
          component: PermissionsComponent,
          data: {
            'permissionCode': ['EditPermissions'],
            'breadcrumb': ['settings', 'permissions', 'edit']
          }
        },
      ]
    },
    { path: 'localization', component: SettingsWrapperComponent,
      children: [
        {
          path: '',
          component: LocalizationListComponent,
          data: {
            permissionCode: ['ViewLocalization'],
            'breadcrumb': ['settings', 'localization']
          }
        },
        { path: 'add', component: LocalizationComponent,
          data: {'breadcrumb': ['settings', 'localization', 'add']}
        },
        {
          path: 'edit/:localizationId',
          component: LocalizationComponent,
          data: {
            'permissionCode': ['EditLocalization'],
            'breadcrumb': ['settings', 'localization', 'edit']
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
