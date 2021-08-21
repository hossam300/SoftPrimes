import { Component, OnInit } from '@angular/core';
import { SettingsCrudsService } from '../settings-cruds.service';

@Component({
  selector: 'app-agents-list',
  templateUrl: './agents-list.component.html',
  styleUrls: ['./agents-list.component.css']
})
export class AgentsListComponent implements OnInit {
  agentsList: any[];
  options: any;
  take = 10; // pageSize
  skip = 0;
  controller = '';
  count: number;

  constructor(private settingsCrud: SettingsCrudsService) {
    this.options = {
      controller: 'Agents',
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
    this.getAgentsList(this.controller, this.take, this.skip);
  }

  getAgentsList(controller, take, skip) {
    this.settingsCrud.getAll(controller, take, skip).subscribe(result => {
      this.agentsList = result.data;
      this.count = result.count;
    });
  }

}
