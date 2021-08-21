import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AgentDTO } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { SettingsCrudsService } from '../settings-cruds.service';

@Component({
  selector: 'app-agents',
  templateUrl: './agents.component.html',
  styleUrls: ['./agents.component.css']
})
export class AgentsComponent implements OnInit {
  agents: AgentDTO;
  routerSubscription: Subscription;
  createMode: boolean;
  controller = 'Agents';

  constructor(
    private settingsCrud: SettingsCrudsService,
    private route: ActivatedRoute,
    private router: Router
  ) {
  }

  ngOnInit() {
    this.routerSubscription = this.route.params.subscribe(r => {
      if (!r.agentsId) {
        this.createMode = true;
        this.agents = new AgentDTO();

      } else {
        this.createMode = false;
        this.settingsCrud.getDTOById(this.controller, +r.agentsId).subscribe(agents => {
          this.agents = agents;
        });
      }
    });
  }

  updateAgent() {
    this.settingsCrud.updateDTO(this.controller, [this.agents]).subscribe(result => {
      if (result) {
        this.router.navigate(['/settings/agents']);
      }
    });
  }

  insertAgent() {
    this.settingsCrud.insertDTO(this.controller, [this.agents]).subscribe(result => {
      if (result) {
        this.router.navigate(['/settings/agents']);
      }
    });
  }

}
