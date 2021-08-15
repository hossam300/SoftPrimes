import { SettingsWrapperComponent } from './settings-wrapper/settings-wrapper.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../core/_guards/auth.guard';

import { SettingsComponent } from './settings/settings.component';
import { PermissionsComponent } from './permissions/permissions.component';

const routes: Routes = [{
  path: '', component: SettingsWrapperComponent,
  canActivate: [AuthGuard], canActivateChild: [AuthGuard],
  children: [
    { path: '', redirectTo: 'settings', pathMatch: 'full', data: {'breadcrumb': ['settings']} },
    { path: 'settings', component: SettingsComponent, data: {'breadcrumb': ['settings']} },
    { path: 'permissions', component: PermissionsComponent, data: {'breadcrumb': ['settings', 'permissions']} },
    { path: '**', redirectTo: 'settings', pathMatch: 'full' },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SettingsRoutingModule { }
