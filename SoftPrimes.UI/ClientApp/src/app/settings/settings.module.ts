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


@NgModule({
  declarations: [SettingsComponent, PermissionsComponent, SettingsWrapperComponent, PermissionsListComponent, SettingsTableComponent, LocalizationComponent, LocalizationListComponent],
  imports: [
    CommonModule,
    SettingsRoutingModule,
    SharedModule,
    FormsModule
  ]
})
export class SettingsModule { }
