import { Filter } from './../../core/_services/swagger/SwaggerClient.service';
import { TaskManagementService } from './../../core/_services/task-management.service';
import { Component, OnInit } from '@angular/core';
import { Sort, TourAgentDTO } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { Marker } from './../../shared/gmap/gmap.component';
import { TourState, TourTypes } from 'src/app/core/_models/task-management';

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
  markers: Marker[] = [];
  states = TourState;
  types = TourTypes;
  tourStates: Filter[] = this.convertEnumToFiltersArray(TourState, 'tourState', 'or');
  tourTypes: Filter[] = this.convertEnumToFiltersArray(TourTypes, 'tourType', 'or');
  filters = {
    tourState: [],
    tourType: [],
    tourName: '',
    agentName: ''
  };

  constructor(
    private taskManagementService: TaskManagementService
  ) { }

  ngOnInit() {
    this.getAll(this.take, this.skip);
    this.initTableColumns();
  }

  convertEnumToFiltersArray(enm, field, operator) {
    const filters = [];

    for (const [propertyKey, propertyValue] of Object.entries(enm)) {
          if (!Number.isNaN(Number(propertyKey))) {
            continue;
        }
        const filter = new Filter({ field: field, operator: 'eq', value: (<string>propertyValue) });
        filters.push(filter);
    }
    return filters;
  }

  initTableColumns() {
    this.options = {
      controller: 'TourAgents',
      columns: [
        { name: 'tourName', field: 'tourNameEn', sortField: 'tour.tourNameEn',  type: 'tour', sort: 'asc' },
        { name: 'agentName', field: 'fullNameEn', sortField: 'agent.fullNameEn', type: 'agent', sort: 'asc' },
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

  buildFilter(event, inputType) {
    if (!event.target.value) {
      this.filters[inputType] = '';
      return;
    }
    this.filters[inputType] = new Filter(
      { field: inputType === 'agentName' ? 'agent.fullNameEn' : 'tour.tourNameEn',
      operator: 'eq', value: (event.target.value) }
    );
  }

  applyFilters() {
    const filters = [...this.filters.tourState, ...this.filters.tourType];
    if (this.filters.tourName) {
      filters.push(this.filters.tourName);
    }
    if (this.filters.agentName) {
      filters.push(this.filters.agentName);
    }
    console.log(filters, 'filters');
    this.getAll(this.take, this.skip, [], filters);
  }

  resetFilters() {
    this.filters = {
      tourState: [],
      tourType: [],
      tourName: '',
      agentName: ''
    };
    this.getAll(this.take, this.skip);
  }

  // getCheckPoints() {
  //   this.toursList.forEach(tour => {
  //     tour.checkPoints.forEach(checkPoint => {
  //       this.markers.push({
  //         lat: checkPoint.lat,
  //         lng:,
  //         label:,
  //         draggable: false
  //       });
  //     });
  //   });
  // }

}
