import { Subject } from 'rxjs/internal/Subject';
import { Component, OnInit } from '@angular/core';
import { SettingsCrudsService } from '../settings-cruds.service';
import { LoaderService } from 'src/app/core/_services/loader.service';

@Component({
  selector: 'app-permissions-list',
  templateUrl: './permissions-list.component.html',
  styleUrls: ['./permissions-list.component.css']
})
export class PermissionsListComponent implements OnInit {
  permissionsList: any[];
  options: any;
  take = 10; // pageSize
  skip = 0;
  controller = '';
  count: number;

  constructor(
    private settingsCrud: SettingsCrudsService,
  ) {
    this.options = {
      controller: 'Permissions',
      columns: [
        { name: 'nameAr', field: 'permissionNameAr', searchable: true, operator: 'contains' },
        { name: 'nameEn', field: 'permissionNameEn', searchable: true, operator: 'contains' },
        { name: 'permissionKey', field: 'permissionKey', searchable: true, operator: 'contains' },
        { name: '', field: '' },
      ]
    };
    this.controller = this.options.controller;
  }

  ngOnInit() {
    this.getPermissionsList(this.controller, this.take, this.skip);
  }

  getPermissionsList(controller, take, skip) {
    // this.loader.addLoader();
    this.settingsCrud.getAll(controller, take, skip).subscribe(result => {
      // this.loader.removeLoader();;
      this.permissionsList = result.data;
      this.count = result.count;
    });
  }

}
