import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/core/_services/loader.service';
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

  constructor(
    private settingsCrud: SettingsCrudsService,
  ) {
    this.options = {
      controller: 'Templates',
      columns: [
        { name: 'tourName', field: 'tourNameEn', searchable: true, operator: 'contains' },
        // { name: 'checkPoints', field: 'checkPoints', searchable: true, operator: 'contains', type: 'length' },
        { name: 'active', field: 'active', searchable: true, operator: 'contains' },
        { name: '', field: '', type: 'templates' },
      ]
    };
    this.controller = this.options.controller;
  }

  ngOnInit() {
    this.getTemplatesList(undefined, this.take);
  }

  getTemplatesList(searchTxt?, take?) {
    // this.loader.addLoader();
    this.settingsCrud.getTemplatesLookup(searchTxt, take).subscribe(result => {
      // this.loader.removeLoader();;
      this.templatesList = result;
    });
  }

}
