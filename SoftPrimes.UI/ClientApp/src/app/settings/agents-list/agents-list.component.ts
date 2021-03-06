import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/core/_services/loader.service';
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

  constructor(
    private settingsCrud: SettingsCrudsService,
  ) {
    this.options = {
      controller: 'Agents',
      columns: [
        { name: 'nameEn', field: 'fullNameEn', searchable: true, operator: 'contains' },
        { name: 'nameAr', field: 'fullNameAr', searchable: true, operator: 'contains' },
        { name: 'email', field: 'email', searchable: true, operator: 'contains' },
        { name: 'jobTitle', field: 'jobTitle', searchable: true, operator: 'contains' },
        { name: 'agentType', field: 'agentType', type: 'agentType', searchable: true, operator: 'contains' },
        { name: '', field: '' },
      ]
    };
    this.controller = this.options.controller;
  }

  ngOnInit() {
    this.getAgentsList(this.controller, this.take, this.skip);
  }

  getAgentsList(controller, take, skip) {
    // this.loader.addLoader();
    this.settingsCrud.getAll(controller, take, skip).subscribe(result => {
      // this.loader.removeLoader();;
      this.agentsList = result.data;
      this.count = result.count;
    });
  }

}
