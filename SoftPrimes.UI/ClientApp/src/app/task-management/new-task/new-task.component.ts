import { Component, OnInit } from '@angular/core';
import {NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-new-task',
  templateUrl: './new-task.component.html',
  styleUrls: ['./new-task.component.css']
})
export class NewTaskComponent implements OnInit {

  tourType = '1';
  tourName = '';
  tourDate: NgbDateStruct;
  checkPointId: number;
  startTime: string;
  endTime: string;
  agentId: number;
  captureLocation: string;
  saveTemplate: false;

  constructor() { }

  ngOnInit() {
  }

  assignTask() {
    const task = {
      tourType: this.tourType,
      tourName: this.tourName,
      tourDate: this.tourDate,
      pointLocation: {
        id: this.checkPointId,
        startTime: this.startTime,
        endTime: this.endTime
      },
      agenId: this.agentId,
      captureLocation: this.captureLocation
    };
    console.log(task, 'task');
  }

  onDateSelect(event) {
    console.log(event);
  }

}
