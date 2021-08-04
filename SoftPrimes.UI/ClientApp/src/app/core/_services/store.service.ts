import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
// import { ReplaySubject } from 'rxjs/internal/ReplaySubject';
// import { Subject } from 'rxjs/internal/Subject';

interface CalendarType {
  isHijri: boolean;
  isGeorgian: boolean;
}

export interface DelegationGroup {
  isDelegationGroup: boolean;
  transactionsIDs: Array<Array<number | string>>;
  isCCGroup?: boolean;
  isDecisionGroup?: boolean;
  from?: string;
}

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  loggedUser$ = new BehaviorSubject(null);

  constructor(private router: Router) { }
}
