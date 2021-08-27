import { TaskManagementService } from './../../core/_services/task-management.service';
import { Component, OnInit } from '@angular/core';
import { Sort, TourAgentDTO } from 'src/app/core/_services/swagger/SwaggerClient.service';

@Component({
  selector: 'app-tasks-list',
  templateUrl: './tasks-list.component.html',
  styleUrls: ['./tasks-list.component.css']
})
export class TasksListComponent implements OnInit {
  toursList: TourAgentDTO[];
  options: any;
  take = 10; // pageSize
  skip = 0;
  count: number;

  constructor(
    private taskManagementService: TaskManagementService
  ) { }

  ngOnInit() {
    this.getAll(this.take, this.skip);
    this.initTableColumns();
  }

  initTableColumns() {
    this.options = {
      controller: 'TourAgents',
      columns: [
        { name: 'tourName', field: 'tourNameEn', sortField: 'tour["tourNameEn"]',  type: 'tour', sort: 'asc' },
        { name: 'agentName', field: 'fullNameEn', sortField: 'agent["fullNameEn"]', type: 'agent', sort: 'asc' },
        { name: 'tourType', field: 'tourType', sortField: 'tourType', type: 'tourType', sort: 'asc' },
        { name: 'scheduleStart', field: 'tourDate', sortField: 'tourDate', type: 'date', sort: 'asc' },
        { name: 'scheduleEnd', field: 'estimatedEndDate', sortField: 'estimatedEndDate', type: 'date', sort: 'asc' },
        { name: 'tourStatus', field: 'tourState', sortField: 'tourState', type: 'tourState', sort: 'asc' },
        { name: '', field: '' },
      ]
    };
  }

  getAll(take, skip, sort = [], filters = []) {
    this.taskManagementService.getAllTourAgents(take, skip, sort, filters).subscribe(result => {
      console.log(result, 'tourAgents');
      this.toursList = result.data;
      this.count = result.count;
    });
  }

  sort(event) {
    const direction = event.sort === 'desc' ? 'asc' : 'desc';
    const sort = [new Sort({ field: event.sortField, dir: direction })];
    console.log(event, sort, 'start sorting');
    this.getAll(this.take, this.skip, sort);
  }

}
