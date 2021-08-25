import { AgentsListComponent } from './agents-list/agents-list.component';
import { RolesComponent } from './roles/roles.component';
import { RolesListComponent } from './roles-list/roles-list.component';
import { LocalizationComponent } from './localization/localization.component';
import { LocalizationListComponent } from './localization-list/localization-list.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../core/_guards/auth.guard';

import { SettingsComponent } from './settings/settings.component';
import { SettingsWrapperComponent } from './settings-wrapper/settings-wrapper.component';
import { PermissionsListComponent } from './permissions-list/permissions-list.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { AgentsComponent } from './agents/agents.component';
import { TemplatesListComponent } from './templates-list/templates-list.component';
import { CheckpointsComponent } from './checkpoints/checkpoints.component';
import { CheckpointsListComponent } from './checkpoints-list/checkpoints-list.component';

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
    { path: 'roles', component: SettingsWrapperComponent,
      children: [
        {
          path: '',
          component: RolesListComponent,
          data: {
            permissionCode: ['ViewRoles'],
            'breadcrumb': ['settings', 'roles']
          }
        },
        { path: 'add', component: RolesComponent,
          data: {'breadcrumb': ['settings', 'roles', 'add']}
        },
        {
          path: 'edit/:rolesId',
          component: RolesComponent,
          data: {
            'permissionCode': ['EditRoles'],
            'breadcrumb': ['settings', 'roles', 'edit']
          }
        },
      ]
    },
    { path: 'agents', component: SettingsWrapperComponent,
      children: [
        {
          path: '',
          component: AgentsListComponent,
          data: {
            permissionCode: ['ViewAgents'],
            'breadcrumb': ['settings', 'agents']
          }
        },
        { path: 'add', component: AgentsComponent,
          data: {'breadcrumb': ['settings', 'agents', 'add']}
        },
        {
          path: 'edit/:agentId',
          component: AgentsComponent,
          data: {
            'permissionCode': ['EditAgents'],
            'breadcrumb': ['settings', 'agents', 'edit']
          }
        },
      ]
    },
    { path: 'checkpoints', component: SettingsWrapperComponent,
      children: [
        {
          path: '',
          component: CheckpointsListComponent,
          data: {
            permissionCode: ['ViewCheckpoints'],
            'breadcrumb': ['settings', 'checkpoints']
          }
        },
        { path: 'add', component: CheckpointsComponent,
          data: {'breadcrumb': ['settings', 'checkpoints', 'add']}
        },
        {
          path: 'edit/:checkPointId',
          component: CheckpointsComponent,
          data: {
            'permissionCode': ['EditCheckpoints'],
            'breadcrumb': ['settings', 'checkpoints', 'edit']
          }
        },
      ]
    },
    { path: 'templates', component: SettingsWrapperComponent,
      children: [
        {
          path: '',
          component: TemplatesListComponent,
          data: {
            permissionCode: ['ViewTemplates'],
            'breadcrumb': ['settings', 'templates']
          }
        }
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
