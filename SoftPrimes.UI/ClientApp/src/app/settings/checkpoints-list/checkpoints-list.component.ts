import { Component, OnInit } from '@angular/core';
import { SettingsCrudsService } from '../settings-cruds.service';

@Component({
  selector: 'app-checkpoints-list',
  templateUrl: './checkpoints-list.component.html',
  styleUrls: ['./checkpoints-list.component.css']
})
export class CheckpointsListComponent implements OnInit {
  checkpointsList: any[];
  options: any;
  take = 10; // pageSize
  skip = 0;
  controller = '';
  count: number;

  constructor(private settingsCrud: SettingsCrudsService) {
    this.options = {
      controller: 'CheckPoints',
      columns: [
        { name: 'nameAr', field: 'checkPointNameAr', searchable: true, operator: 'contains' },
        { name: 'nameEn', field: 'checkPointNameEn', searchable: true, operator: 'contains' },
        { name: 'CheckPointNamelat', field: 'lat', searchable: true, operator: 'contains' },
        { name: 'CheckPointNamelong', field: 'long', searchable: true, operator: 'contains' },
        { name: 'LocationText', field: 'locationText', searchable: true, operator: 'contains' },
        { name: 'QrCode', field: '', searchable: true, operator: 'contains' },
        { name: '', field: '' },
      ]
    };
    this.controller = this.options.controller;
  }

  ngOnInit() {
    this.getCheckpointsList(this.controller, this.take, this.skip);
  }

  getCheckpointsList(controller, take, skip) {
    this.settingsCrud.getAll(controller, take, skip).subscribe(result => {
      this.checkpointsList = result.data;
      this.count = result.count;
    });
  }

}
