import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/core/_services/loader.service';
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

  constructor(
    private settingsCrud: SettingsCrudsService,
    private loader: LoaderService
  ) {
    this.options = {
      controller: 'Roles',
      columns: [
        { name: 'nameAr', field: 'roleNameAr', searchable: true, operator: 'contains' },
        { name: 'nameEn', field: 'roleNameEn', searchable: true, operator: 'contains' },
        { name: 'permissions', field: 'permissions', type: 'arr', searchable: true, operator: 'contains' },
        { name: '', field: '' },
      ]
    };
    this.controller = this.options.controller;
  }

  ngOnInit() {
    this.getRolesList(this.controller, this.take, this.skip);
  }

  getRolesList(controller, take, skip) {
    this.loader.addLoader();
    this.settingsCrud.getAll(controller, take, skip).subscribe(result => {
      this.loader.removeLoader();
      this.rolesList = result.data;
      this.count = result.count;
    });
  }

}
