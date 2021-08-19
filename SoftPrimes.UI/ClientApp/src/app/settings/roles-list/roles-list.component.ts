import { Component, OnInit } from '@angular/core';
import { SettingsCrudsService } from '../settings-cruds.service';

@Component({
  selector: 'app-roles-list',
  templateUrl: './roles-list.component.html',
  styleUrls: ['./roles-list.component.css']
})
export class RolesListComponent implements OnInit {
  rolesList: any[];
  options: any;
  take = 10; // pageSize
  skip = 0;
  controller = '';
  count: number;

  constructor(private settingsCrud: SettingsCrudsService) {
    this.options = {
      controller: 'Roles',
      columns: [
        { name: 'valueAr', field: 'valueAr', searchable: true, operator: 'contains' },
        { name: 'valueEn', field: 'valueEn', searchable: true, operator: 'contains' },
        { name: 'key', field: 'key', searchable: true, operator: 'contains' },
        { name: '', field: '' },
      ]
    };
    this.controller = this.options.controller;
  }

  ngOnInit() {
    this.getRolesList(this.controller, this.take, this.skip);
  }

  getRolesList(controller, take, skip) {
    this.settingsCrud.getAll(controller, take, skip).subscribe(result => {
      this.rolesList = result.data;
      this.count = result.count;
    });
  }

}
