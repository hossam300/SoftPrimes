import { SettingsCrudsService } from './../settings-cruds.service';
import { PermissionDTO } from './../../core/_services/swagger/SwaggerClient.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { LoaderService } from 'src/app/core/_services/loader.service';

@Component({
  selector: 'app-permissions',
  templateUrl: './permissions.component.html',
  styleUrls: ['./permissions.component.css']
})
export class PermissionsComponent implements OnInit {
  permission: PermissionDTO;
  routerSubscription: Subscription;
  createMode: boolean;
  controller = 'Permissions';

  constructor(
    private settingsCrud: SettingsCrudsService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.routerSubscription = this.route.params.subscribe(r => {
      if (!r.permissionId) {
        this.createMode = true;
        this.permission = new PermissionDTO();

      } else {
        this.createMode = false;
        this.settingsCrud.getDTOById(this.controller, +r.permissionId).subscribe(permission => {
          this.permission = permission;
        });
      }
    });
  }

  updatePermission() {
    // this.loader.addLoader();
    this.settingsCrud.updateDTO(this.controller, [this.permission]).subscribe(result => {
      // this.loader.removeLoader();;
      if (result) {
        this.router.navigate(['/settings/permissions']);
      }
    });
  }

  insertPermission() {
    // this.loader.addLoader();
    this.settingsCrud.insertDTO(this.controller, [this.permission]).subscribe(result => {
      // this.loader.removeLoader();;
      if (result) {
        this.router.navigate(['/settings/permissions']);
      }
    });
  }

}
