import { MapSettings, Marker } from 'src/app/core/_models/gmap';
import { CheckPointDTO } from './../../core/_services/swagger/SwaggerClient.service';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SettingsCrudsService } from '../settings-cruds.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LoaderService } from 'src/app/core/_services/loader.service';

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
  marker: Marker[];
  mapSettings: MapSettings;

  constructor(
    private settingsCrud: SettingsCrudsService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.checkPoint = new CheckPointDTO();
    this.routerSubscription = this.route.params.subscribe(r => {
      if (!r.checkPointId) {
        this.createMode = true;

      } else {
        this.createMode = false;
        this.settingsCrud.getDTOById(this.controller, +r.checkPointId).subscribe(checkPoint => {
          this.checkPoint = checkPoint;
          this.marker = [{
            lat: checkPoint.lat,
            lng: checkPoint.long
          }];
          this.mapSettings = {
            lat: checkPoint.lat,
            lng: checkPoint.long,
            zoom: 17,
            zoomControl: false
          };
        });
      }
    });
  }

  getMarker($event) {
    this.checkPoint.lat = $event.lat;
    this.checkPoint.long = $event.lng;
  }

  updateCheckPoint() {
    // this.loader.addLoader();
    this.settingsCrud.updateDTO(this.controller, [this.checkPoint]).subscribe(result => {
      // this.loader.removeLoader();;
      if (result) {
        this.router.navigate(['/settings/checkpoints']);
      }
    });
  }

  insertCheckPoint() {
    // this.loader.addLoader();
    this.settingsCrud.insertDTO(this.controller, [this.checkPoint]).subscribe(result => {
      // this.loader.removeLoader();;
      if (result) {
        this.router.navigate(['/settings/checkpoints']);
      }
    });
  }

}
