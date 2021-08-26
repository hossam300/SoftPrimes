import { TaskManagementService } from './../../core/_services/task-management.service';
import { Component, OnInit } from '@angular/core';
import { TourAgentDTO } from 'src/app/core/_services/swagger/SwaggerClient.service';

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
        { name: 'tourName', field: 'tourNameEn', type: 'tour' },
        { name: 'agentName', field: 'fullNameAr', type: 'agent' },
        { name: 'tourType', field: 'tourType', type: 'tourType' },
        { name: 'scheduleStart', field: 'tourDate', type: 'date' },
        { name: 'scheduleEnd', field: 'estimatedEndDate', type: 'date' },
        { name: 'tourStatus', field: 'tourState', type: 'tourState' },
        { name: '', field: '' },
      ]
    };
  }

  getAll(take, skip) {
    this.taskManagementService.getAllTourAgents(take, skip).subscribe(result => {
      console.log(result, 'tourAgents');
      this.toursList = result.data;
      this.count = result.count;
    });
  }

}
