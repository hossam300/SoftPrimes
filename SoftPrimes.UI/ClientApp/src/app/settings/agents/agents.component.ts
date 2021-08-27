import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { CompanyDTO, RoleDTO, AgentDetailsDTO } from './../../core/_services/swagger/SwaggerClient.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { concat, Observable, of, Subject, Subscription } from 'rxjs';
import { AgentDTO } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { SettingsCrudsService } from '../settings-cruds.service';
import { catchError, debounceTime, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';

export enum AgentType {
  NormalAgent = 1,
  Supervisor = 2,
  Admin = 3
}

@Component({
  selector: 'app-agents',
  templateUrl: './agents.component.html',
  styleUrls: ['./agents.component.css']
})
export class AgentsComponent implements OnInit {
  agent: AgentDetailsDTO;
  routerSubscription: Subscription;
  createMode: boolean;
  controller = 'Agents';

  birthdate: NgbDateStruct;

  agentType = AgentType;
  agentTypes = [AgentType.NormalAgent, AgentType.Supervisor, AgentType.Admin];

  roles$: Observable<RoleDTO[]>;
  rolesInput$ = new Subject<string>();
  rolesLoading = false;

  agents$: Observable<AgentDTO[]>;
  agentsInput$ = new Subject<string>();
  agentsLoading = false;

  company$: Observable<CompanyDTO[]>;
  companyInput$ = new Subject<string>();
  companyLoading = false;

  roleId: number;

  constructor(
    private settingsCrud: SettingsCrudsService,
    private route: ActivatedRoute,
    private router: Router
  ) {
  }

  ngOnInit() {
    // get lookups
    this.getRoles();
    this.getAgents();
    this.getCompanies();

    this.routerSubscription = this.route.params.subscribe(r => {
      if (!r.agentId) {
        this.createMode = true;
        this.agent = new AgentDTO();

      } else {
        this.createMode = false;
        this.settingsCrud.getUserProfile(r.agentId).subscribe(agents => {
          this.agent = agents;
        });
      }
    });
  }

  updateAgent() {
    this.settingsCrud.updateAgent(this.agent).subscribe(result => {
      if (result) {
        this.router.navigate(['/settings/agents']);
      }
    });
  }

  insertAgent() {
    this.agent.birthDate = this.getDate(this.birthdate);
    this.agent.roleId = this.roleId;
    console.log(this.agent, 'fucken agent');
    this.settingsCrud.insertAgent(this.roleId,this.agent).subscribe(res => {
      if (res) {
        this.router.navigate(['/settings/agents']);
      }
    });
  }

  getRoles() {
    this.settingsCrud.getRolesLookup().subscribe(value => {
      this.roles$ = concat(
        of(value), // default items
        this.rolesInput$.pipe(
          debounceTime(200),
          distinctUntilChanged(),
          tap(() => this.rolesLoading = true),
          switchMap(term => this.settingsCrud.getRolesLookup(term).pipe(
            catchError(() => of([])), // empty list on error
            tap(() => this.rolesLoading = false)
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

  getCompanies() {
    this.settingsCrud.getCompaniesLookup().subscribe(value => {
      this.company$ = concat(
        of(value), // default items
        this.companyInput$.pipe(
          debounceTime(200),
          distinctUntilChanged(),
          tap(() => this.companyLoading = true),
          switchMap(term => this.settingsCrud.getCompaniesLookup(term).pipe(
            catchError(() => of([])), // empty list on error
            tap(() => this.companyLoading = false)
          ))
        )
      );
    });
  }

  getDate(date) {
    return new Date(date.year, date.month, date.day);
  }

}
