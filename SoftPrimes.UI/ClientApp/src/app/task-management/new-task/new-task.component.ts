import { SettingsCrudsService } from './../../settings/settings-cruds.service';
import { Component, OnInit } from '@angular/core';
import {NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';
import { concat, of, Subject } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';

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
  capturesLookup = [10, 20, 30, 40, 50, 60];

  createMode = true;

  templates$ = of([]);
  templatesInput$ = new Subject<string>();
  templatesLoading = false;

  agents$ = of([]);
  agentsInput$ = new Subject<string>();
  agentsLoading = false;

  constructor(private settingsCrud: SettingsCrudsService) { }

  ngOnInit() {
    this.getTemplates();
    this.getAgents();
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

}
