import { Filter } from './../../core/_services/swagger/SwaggerClient.service';
import { TaskManagementService } from './../../core/_services/task-management.service';
import { Component, OnInit } from '@angular/core';
import { Sort, TourAgentDTO } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { TourState, TourTypes } from 'src/app/core/_models/task-management';
import { Marker } from 'src/app/core/_models/gmap';

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
  sortArr: Sort[] = [];
  filtersArr: Filter[] = [];
  states = TourState;
  types = TourTypes;
  tourStates: Filter[] = this.convertEnumToFiltersArray(TourState, 'tourState', 'eq');
  tourTypes: Filter[] = this.convertEnumToFiltersArray(TourTypes, 'tourType', 'eq');
  tourName = '';
  agentName = '';
  filters = {
    tourState: [],
    tourType: null,
    tourName: '',
    agentName: ''
  };

  constructor(
    private taskManagementService: TaskManagementService
  ) { }

  ngOnInit() {
    this.initTableColumns();
    this.getAll(this.take, this.skip);
    this.getAgentCheckPoints();
  }

  convertEnumToFiltersArray(enm, field, operator) {
    const filters = [];

    for (const [propertyKey, propertyValue] of Object.entries(enm)) {
          if (!Number.isNaN(Number(propertyKey))) {
            continue;
        }
        const filter = new Filter({ field: field, operator: operator, value: (<string>propertyValue), logic: 'and' });
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

  getAll(take, skip, sort = [], filters = [], sortField?, sortDir?) {
    this.taskManagementService.getAllTourAgents(take, skip, sort, filters, sortField, sortDir).subscribe(result => {
      this.toursList = result.data;
      this.count = result.count;
    });
  }

  sort(event) {
    const direction = event.sort === 'desc' ? 'asc' : 'desc';
    // this.sortArr = [new Sort({ field: event.sortField, dir: direction })];
    this.getAll(this.take, this.skip, [], this.filtersArr, event.sortField, direction);
  }

  buildFilter(event?, inputType?) {
    if (event) {
      if (!event.target.value) {
        this.filters[inputType] = '';
        return;
      }
      this.filters[inputType] = new Filter(
        { field: inputType === 'agentName' ? 'agent.fullNameEn' : 'tour.tourNameEn',
        operator: 'eq', value: (event.target.value), logic: 'and' }
      );
    }

    const filters = [...this.filters.tourState, this.filters.tourType];
    if (this.filters.tourName) {
      filters.push(this.filters.tourName);
    }
    if (this.filters.agentName) {
      filters.push(this.filters.agentName);
    }
    this.filtersArr = filters;
  }

  applyFilters() {
    this.buildFilter();
    this.getAll(this.take, this.skip, [], this.filtersArr);
  }

  resetFilters() {
    this.filters = {
      tourState: [],
      tourType: null,
      tourName: '',
      agentName: ''
    };
    this.tourName = '';
    this.agentName = '';
    this.getAll(this.take, this.skip);
  }

  getAgentCheckPoints() {
    this.taskManagementService.getAgentCheckPoints().subscribe(res => {
      const data = this.convertAOOToOOA(res);
      const finalArr: Marker[] = [];
      for (const [key, val] of Object.entries(data)) {
        const nxt = [];
        val.forEach((x, i) => {
          if (i > 0) {
            nxt.push({
              locationText: x.locationText || x.checkPointNameEn,
              distanceToNextPoint: x.distanceToNextPoint,
            });
          }
        });
        const item: Marker = {
          label: val[0].agent.fullNameEn,
          lat: val[0].lat,
          lng: val[0].long,
          current: {
            locationText: val[0].locationText || val[0].checkPointNameEn,
            distanceToNextPoint: val[0].distanceToNextPoint,
          },
          next: nxt
        };
        finalArr.push(item);
      }
      this.markers = finalArr;
    });
  }

  convertAOOToOOA(arrOfObjects: any[]) {
    const objOfArraies = new Object();
    arrOfObjects.forEach(x => {
        if (objOfArraies[x.id]) {
            objOfArraies[x.id].push(x);
        } else {
            objOfArraies[x.id] = [x];
        }
    });
    return objOfArraies;
  }

}
