import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SettingsRoutingModule } from './settings-routing.module';
import { SharedModule } from './../shared/shared.module';

import { SettingsComponent } from './settings/settings.component';
import { PermissionsComponent } from './permissions/permissions.component';
import { SettingsWrapperComponent } from './settings-wrapper/settings-wrapper.component';


@NgModule({
  declarations: [SettingsComponent, PermissionsComponent, SettingsWrapperComponent],
  imports: [
    CommonModule,
    SettingsRoutingModule,
    SharedModule
  ]
})
export class SettingsModule { }
