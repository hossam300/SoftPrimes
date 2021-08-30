import { TaskManagementService } from './../../core/_services/task-management.service';
import { TourCreateDTO, TourType, PointLocationDTO, TourTemplateDTO } from './../../core/_services/swagger/SwaggerClient.service';
import { SettingsCrudsService } from './../../settings/settings-cruds.service';
import { Component, OnInit } from '@angular/core';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { concat, of, Subject, Subscription } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, map, switchMap, tap, throwIfEmpty } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-new-task',
  templateUrl: './new-task.component.html',
  styleUrls: ['./new-task.component.css']
})
export class NewTaskComponent implements OnInit {

  tour: TourCreateDTO = new TourCreateDTO();
  tourDate: NgbDateStruct;
  checkPoints = [];
  pointLocations: PointLocationDTO[] = [];
  startTime: string;
  endTime: string;
  capturesLookup = [10, 20, 30, 40, 50, 60];

  markers = [];

  createMode = true;

  templates$ = of([]);
  templatesInput$ = new Subject<string>();
  templatesLoading = false;

  agents$ = of([]);
  agentsInput$ = new Subject<string>();
  agentsLoading = false;

  checkPoints$ = of([]);
  checkPointsInput$ = new Subject<string>();
  checkPointsLoading = false;

  routerSubscription: Subscription;
  controller = 'Tours';

  constructor(
    private taskManagement: TaskManagementService,
    private settingsCrud: SettingsCrudsService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    // get lookups
    this.getTemplates();
    this.getAgents();
    this.getCheckPoints();

    this.routerSubscription = this.route.params.subscribe(r => {
      if (!r.tourId) {
        this.createMode = true;
        this.tour = new TourCreateDTO();
        this.tour.tourType = TourType._1;
      } else {
        this.createMode = false;
        this.settingsCrud.getDTOById(this.controller, +r.tourId).subscribe(tour => {
          this.tour = tour;
        });
      }
    });
  }

  assignTask() {
    this.tour.tourDate = this.getDate(this.tourDate);
    this.tour.pointLocations = [];
    this.checkPoints.forEach(x => {
      const location = new PointLocationDTO({
        checkPointId: x.id,
        startDate: x.startDate,
        endDate: x.endDate
      });
      this.tour.pointLocations.push(location);
    });
    console.log(this.checkPoints, 'checkPoint');
    console.log(this.tour, 'task');
    this.taskManagement.insertTour(this.tour).subscribe( value => {
      if (value) {
        this.router.navigate(['/task-management/tasks-list']);
      }
    });
  }

  updateTask() {
    console.log('update');
  }

  onDateSelect(event) {
    console.log(event);
  }

  getTemplates() {
    this.settingsCrud.getTemplatesLookup().subscribe(value => {
      this.templates$ = concat(
        of(value.filter(x => x.active)), // default items
        this.templatesInput$.pipe(
          debounceTime(200),
          distinctUntilChanged(),
          tap(() => this.templatesLoading = true),
          switchMap(term => this.settingsCrud.getTemplatesLookup(term).pipe(
            map(res => res.filter(x => x.active)),
            catchError(() => of([])), // empty list on error
            tap(() => this.templatesLoading = false)
          ))
        )
      );
    });
  }

  getAgents() {
    this.settingsCrud.getAgentsLookup().subscribe(value => {
      this.agents$ = concat(
        of(value), // default items
        this.agentsInput$.pipe(
          debounceTime(200),
          distinctUntilChanged(),
          tap(() => this.agentsLoading = true),
          switchMap(term => this.settingsCrud.getAgentsLookup(term).pipe(
            catchError(() => of([])), // empty list on error
            tap(() => this.agentsLoading = false)
          ))
        )
      );
    });
  }

  getCheckPoints() {
    this.settingsCrud.getCheckPointsLookup().subscribe(value => {
      this.checkPoints$ = concat(
        of(value), // default items
        this.checkPointsInput$.pipe(
          debounceTime(200),
          distinctUntilChanged(),
          tap(() => this.checkPointsLoading = true),
          switchMap(term => this.settingsCrud.getCheckPointsLookup(term).pipe(
            catchError(() => of([])), // empty list on error
            tap(() => this.checkPointsLoading = false)
          ))
        )
      );
    });
  }

  deleteCheckpoint(id) {
    this.checkPoints = this.checkPoints.filter(x => x.id !== id);
  }

  checkPointsChanged($event) {
    this.fixDateTimePickers();
    this.markers = $event.map(x => {
      return {
        lat: x.lat,
        lng: x.long,
        label: x.checkPointNameEn
      };
    });
    if (this.tour.pointLocations && this.tour.pointLocations.length) {
      this.tour.pointLocations.forEach(point => {
        $event = $event.map(val => {
          if (point.checkPointId === val.id) {
            val.startDate = point.startDate;
            val.endDate = point.endDate;
          }
          return val;
        });
      });
    }
  }

  fixDateTimePickers() {
    setTimeout(() => {
      const items =  document.querySelectorAll('.ngx-picker');
      items.forEach(item => {
        item.querySelector('input').classList.add(...['form-control', 'border-radius-left-none', 'border-left-0', 'border-right']);
        item.querySelector('button').classList.add('d-none');
      });
    });
  }

  getDate(date) {
    return new Date(date.year, date.month, date.day);
  }

  chooseTemplate(event: TourTemplateDTO) {
    this.tour.tourName = event.tourNameEn;
    if (event.checkPoints[0]) {
      this.tour.tourId = event.checkPoints[0].tourId;
      this.tour.pointLocations = event.checkPoints.map(point => {
        const location = new PointLocationDTO();
        location.startDate = point.startDate;
        location.endDate = point.endDate;
        location.checkPointId = point.checkPointId;
        return location;
      });
      this.checkPoints = event.checkPoints.map((point: any) => {
        point.checkPoint.startDate = point.startDate;
        point.checkPoint.endDate = point.endDate;
        return point.checkPoint;
      });
      this.checkPointsChanged(this.checkPoints);
    }
  }

}
