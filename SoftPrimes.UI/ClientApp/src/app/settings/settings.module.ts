import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SettingsRoutingModule } from './settings-routing.module';
import { SharedModule } from './../shared/shared.module';
import { FormsModule } from '@angular/forms';

import { SettingsComponent } from './settings/settings.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { SettingsWrapperComponent } from './settings-wrapper/settings-wrapper.component';
import { PermissionsListComponent } from './permissions-list/permissions-list.component';
import { SettingsTableComponent } from './settings-table/settings-table.component';
import { LocalizationComponent } from './localization/localization.component';
import { LocalizationListComponent } from './localization-list/localization-list.component';
import { RolesComponent } from './roles/roles.component';
import { RolesListComponent } from './roles-list/roles-list.component';
import { AgentsComponent } from './agents/agents.component';
import { AgentsListComponent } from './agents-list/agents-list.component';
import { TemplatesListComponent } from './templates-list/templates-list.component';
import { CheckpointsComponent } from './checkpoints/checkpoints.component';
import { CheckpointsListComponent } from './checkpoints-list/checkpoints-list.component';


@NgModule({
  declarations: [
    SettingsComponent,
    PermissionsComponent,
    SettingsWrapperComponent,
    PermissionsListComponent,
    SettingsTableComponent,
    LocalizationComponent,
    LocalizationListComponent,
    RolesComponent,
    RolesListComponent,
    AgentsComponent,
    AgentsListComponent,
    TemplatesListComponent,
    CheckpointsComponent,
    CheckpointsListComponent],
  imports: [
    CommonModule,
    SettingsRoutingModule,
    SharedModule,
    FormsModule
  ]
})
export class SettingsModule { }
