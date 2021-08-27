import { CheckPointDTO } from './../../core/_services/swagger/SwaggerClient.service';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SettingsCrudsService } from '../settings-cruds.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-checkpoints',
  templateUrl: './checkpoints.component.html',
  styleUrls: ['./checkpoints.component.css']
})
export class CheckpointsComponent implements OnInit {
  checkPoint: CheckPointDTO;
  routerSubscription: Subscription;
  createMode: boolean;
  controller = 'CheckPoints';

  constructor(
    private settingsCrud: SettingsCrudsService,
    private route: ActivatedRoute,
    private router: Router
  ) {
  }

  ngOnInit() {
    this.routerSubscription = this.route.params.subscribe(r => {
      if (!r.checkPointId) {
        this.createMode = true;
        this.checkPoint = new CheckPointDTO();

      } else {
        this.createMode = false;
        this.settingsCrud.getDTOById(this.controller, +r.checkPointId).subscribe(checkPoint => {
          this.checkPoint = checkPoint;
        });
      }
    });
  }

  getMarker($event) {
    this.checkPoint.lat = $event.lat;
    this.checkPoint.long = $event.lng;
  }

  updateCheckPoint() {
    if (!this.checkPoint.locationText) {
      this.checkPoint.locationText = this.checkPoint.checkPointNameEn;
    }
    this.settingsCrud.updateDTO(this.controller, [this.checkPoint]).subscribe(result => {
      if (result) {
        this.router.navigate(['/settings/checkpoints']);
      }
    });
  }

  insertCheckPoint() {
    if (!this.checkPoint.locationText) {
      this.checkPoint.locationText = this.checkPoint.checkPointNameEn;
    }
    this.settingsCrud.insertDTO(this.controller, [this.checkPoint]).subscribe(result => {
      if (result) {
        this.router.navigate(['/settings/checkpoints']);
      }
    });
  }

}