import { Subject } from 'rxjs/internal/Subject';
import { Component, OnInit } from '@angular/core';
import { SettingsCrudsService } from '../settings-cruds.service';

@Component({
  selector: 'app-permissions-list',
  templateUrl: './permissions-list.component.html',
  styleUrls: ['./permissions-list.component.css']
})
export class PermissionsListComponent implements OnInit {
  permissionsList: any[];
  options: any;

  constructor(private settingsCrud: SettingsCrudsService) {
    this.options = {
      controller: 'Permissions',
      columns: [
        { name: 'PermissionNameAr', field: 'permissionNameAr', searchable: true, operator: 'contains' },
        { name: 'PermissionNameEn', field: 'permissionNameEn', searchable: true, operator: 'contains' },
        { name: 'PermissionNameKey', field: 'permissionKey', searchable: true, operator: 'contains' },
      ]
    };
  }

  ngOnInit() {
    const permissionController = this.options.controller;
    this.settingsCrud.getAll(permissionController, 10, 0).subscribe(result => {
      this.permissionsList = result.data;
    });
  }

}
