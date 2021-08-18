import { TaskManagementService } from './../../core/_services/task-management.service';
import { Observable } from 'rxjs';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.css']
})
export class DataTableComponent implements OnInit {
  @Input() data: any[];
  @Input() columns: string[];
  @Input() pageSize = 5;
  @Input() count;
  @Output() skip = new EventEmitter<number>();
  currentPage = 1;

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
