import { TaskManagementService } from './../../core/_services/task-management.service';
import { TourCreateDTO, TourType, Filter, PointLocationDTO } from './../../core/_services/swagger/SwaggerClient.service';
import { SettingsCrudsService } from './../../settings/settings-cruds.service';
import { Component, OnInit } from '@angular/core';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { concat, of, Subject, Subscription } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-new-task',
  templateUrl: './new-task.component.html',
  styleUrls: ['./new-task.component.css']
})
export class NewTaskComponent implements OnInit {

  tour: TourCreateDTO;
  tourDate: NgbDateStruct;
  checkPoints = [];
  pointLocations: PointLocationDTO[] = [];
  startTime: string;
  endTime: string;
  capturesLookup = [10, 20, 30, 40, 50, 60];
  timeLookup = [
    {
      id: 1,
      value: '01.00'
    },
    {
      id: 2,
      value: '02.00'
    },
    {
      id: 3,
      value: '03.00'
    },
    {
      id: 4,
      value: '04.00'
    },
    {
      id: 5,
      value: '05.00'
    },
    {
      id: 6,
      value: '06.00'
    },
    {
      id: 7,
      value: '07.00'
    },
    {
      id: 8,
      value: '08.00'
    },
    {
      id: 9,
      value: '09.00'
    },
    {
      id: 10,
      value: '10.00'
    },
    {
      id: 11,
      value: '11.00'
    },
    {
      id: 12,
      value: '12.00'
    }
  ];
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
        startDate: new Date(),
        endDate: new Date()
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
        of(value), // default items
        this.templatesInput$.pipe(
          debounceTime(200),
          distinctUntilChanged(),
          tap(() => this.templatesLoading = true),
          switchMap(term => this.settingsCrud.getTemplatesLookup(term).pipe(
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

  selectChanged($event) {
    this.markers = $event.map(x => {
      return {
        lat: x.lat,
        lng: x.long,
        label: x.checkPointNameEn
      };
    });
    console.log($event, 'location');
    console.log(this.markers, 'location');
  }

  getDate(date) {
    return new Date(date.year, date.month, date.day);
  }

}
