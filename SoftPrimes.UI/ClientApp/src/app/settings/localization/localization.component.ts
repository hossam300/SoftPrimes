import { LocalizationDTO } from './../../core/_services/swagger/SwaggerClient.service';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SettingsCrudsService } from '../settings-cruds.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LoaderService } from 'src/app/core/_services/loader.service';

@Component({
  selector: 'app-localization',
  templateUrl: './localization.component.html',
  styleUrls: ['./localization.component.css']
})
export class LocalizationComponent implements OnInit {
  localization: LocalizationDTO;
  routerSubscription: Subscription;
  createMode: boolean;
  controller = 'Localizations';

  constructor(
    private settingsCrud: SettingsCrudsService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.routerSubscription = this.route.params.subscribe(r => {
      if (!r.localizationId) {
        this.createMode = true;
        this.localization = new LocalizationDTO();

      } else {
        this.createMode = false;
        this.settingsCrud.getDTOById(this.controller, +r.localizationId).subscribe(localization => {
          this.localization = localization;
        });
      }
    });
  }

  updateLocalization() {
    // this.loader.addLoader();
    this.settingsCrud.updateDTO(this.controller, [this.localization]).subscribe(result => {
      // this.loader.removeLoader();;
      if (result) {
        this.router.navigate(['/settings/localization']);
      }
    });
  }

  insertLocalization() {
    // this.loader.addLoader();
    this.settingsCrud.insertDTO(this.controller, [this.localization]).subscribe(result => {
      // this.loader.removeLoader();;
      if (result) {
        this.router.navigate(['/settings/localization']);
      }
    });
  }

}
