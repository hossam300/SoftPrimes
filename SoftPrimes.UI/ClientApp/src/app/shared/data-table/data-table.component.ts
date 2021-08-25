import { TaskManagementService } from './../../core/_services/task-management.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

export enum TourTypes {
  'TourPoints' = 1,
  'Monitoring'
}

export enum TourState {
  New = 1,
  InProgress = 2,
  Complete = 3,
  NotCompleted = 4,
  Cancled = 5
}

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.css']
})
export class DataTableComponent implements OnInit {
  @Input() data: any[];
  @Input() options: any;
  @Input() pageSize = 5;
  @Input() count;
  @Output() skip = new EventEmitter<number>();
  currentPage = 1;
  tourType = TourTypes;
  tourState = TourState;

  constructor(
    private taskManagementService: TaskManagementService
  ) { }

  ngOnInit() {
  }

  editTour(id: number) {
    console.log(id);
  }

  deleteTour(id: number) {
    this.data = this.data.filter(x => x.id !== id);
    const tempRecord = this.data.some(x => x.id === id);
    this.taskManagementService.deleteTourAgent(id).subscribe(result => {
      this.data = this.data.filter(x => x.id !== id);
    }, err => {
      this.data.push(tempRecord);
    });
  }

  emitPagination() {
    const skipVal = (this.currentPage - 1) * this.pageSize;
    this.skip.emit(skipVal);
  }

}
