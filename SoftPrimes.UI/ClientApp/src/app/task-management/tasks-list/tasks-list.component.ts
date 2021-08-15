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
  columns: string[];

  constructor(
    private taskManagementService: TaskManagementService
  ) { }

  ngOnInit() {
    this.getAll();
    this.initTableColumns();
  }

  initTableColumns() {
    this.columns = [
      'agentName',
      'type',
      'scheduleStart',
      'tours',
      'scheduleEnd',
      'scheduleCompleted',
      ''
    ];
  }

  getAll() {
    this.taskManagementService.getAllTourAgents(10, 0).subscribe(result => {
      console.log(result, 'tourAgents');
      this.toursList = result.data;
    });
  }

}
