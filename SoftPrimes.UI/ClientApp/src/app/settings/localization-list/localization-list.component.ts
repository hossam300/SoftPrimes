import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/core/_services/loader.service';
import { SettingsCrudsService } from '../settings-cruds.service';

@Component({
  selector: 'app-localization-list',
  templateUrl: './localization-list.component.html',
  styleUrls: ['./localization-list.component.css']
})
export class LocalizationListComponent implements OnInit {
  localizationList: any[];
  options: any;
  take = 10; // pageSize
  skip = 0;
  controller = '';
  count: number;

  constructor(
    private settingsCrud: SettingsCrudsService,
  ) {
    this.options = {
      controller: 'Localizations',
      columns: [
        { name: 'nameAr', field: 'valueAr', searchable: true, operator: 'contains' },
        { name: 'nameEn', field: 'valueEn', searchable: true, operator: 'contains' },
        { name: 'localizationKey', field: 'key', searchable: true, operator: 'contains' },
        { name: '', field: '' },
      ]
    };
    this.controller = this.options.controller;
  }

  ngOnInit() {
    this.getLocalizationList(this.controller, this.take, this.skip);
  }

  getLocalizationList(controller, take, skip) {
    // this.loader.addLoader();
    this.settingsCrud.getAll(controller, take, skip).subscribe(result => {
      // this.loader.removeLoader();;
      this.localizationList = result.data;
      this.count = result.count;
    });
  }

}
