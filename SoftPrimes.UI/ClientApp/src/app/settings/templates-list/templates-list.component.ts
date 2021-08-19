import { Component, OnInit } from '@angular/core';
import { SettingsCrudsService } from '../settings-cruds.service';

@Component({
  selector: 'app-templates-list',
  templateUrl: './templates-list.component.html',
  styleUrls: ['./templates-list.component.css']
})
export class TemplatesListComponent implements OnInit {
  templatesList: any[];
  options: any;
  take = 10; // pageSize
  skip = 0;
  controller = '';
  count: number;

  constructor(private settingsCrud: SettingsCrudsService) {
    this.options = {
      controller: 'Templates',
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
    this.getTemplatesList(this.controller, this.take, this.skip);
  }

  getTemplatesList(controller, take, skip) {
    this.settingsCrud.getAll(controller, take, skip).subscribe(result => {
      this.templatesList = result.data;
      this.count = result.count;
    });
  }

}
