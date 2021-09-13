import { LoaderService } from './../../core/_services/loader.service';
import { TaskManagementService } from './../../core/_services/task-management.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TourState, TourTypes } from 'src/app/core/_models/task-management';

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
  @Output() sort = new EventEmitter<any>();
  sortDirection = true;
  currentPage = 1;
  tourType = TourTypes;
  tourState = TourState;

  constructor(
    private taskManagementService: TaskManagementService,
    private loader: LoaderService
  ) { }

  ngOnInit() {
  }

  editTour(id: number) {
    console.log(id);
  }

  deleteTour(id: number) {
    this.loader.addLoader();
    this.data = this.data.filter(x => x.id !== id);
    const tempRecord = this.data.some(x => x.id === id);
    this.taskManagementService.deleteTourAgent(id).subscribe(result => {
      this.loader.removeLoader();
      this.data = this.data.filter(x => x.id !== id);
    }, err => {
      this.data.push(tempRecord);
    });
  }

  emitPagination() {
    const skipVal = (this.currentPage - 1) * this.pageSize;
    this.skip.emit(skipVal);
  }

  handleBadgeClass(val) {
    let badgeClass = '';
    switch (val) {
      case 1:
        badgeClass = 'badge-secondary';
        break;
      case 2:
        badgeClass = 'badge-primary';
        break;
      case 3:
        badgeClass = 'badge-success';
        break;
      case 4:
        badgeClass = 'badge-warning';
        break;
      case 5:
        badgeClass = 'badge-danger';
        break;
      default:
        badgeClass = 'badge-primary';
        break;
    }
    return badgeClass;
  }

  sorting(val) {
    this.sort.emit(val);
    this.options.columns = this.options.columns.map(x => {
      if (x.field === val.field) {
        x.sort = val.sort === 'asc' ? 'desc' : 'asc';
      }
      return x;
    });
  }

}
