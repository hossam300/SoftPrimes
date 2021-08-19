import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { RoleDTO } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { SettingsCrudsService } from '../settings-cruds.service';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {
  roles: RoleDTO;
  routerSubscription: Subscription;
  createMode: boolean;
  controller = 'Roles';
  permissionIds;
  permissions = [
    {
      'id': 1,
      'permissionNameAr': 'اضافة مهمة',
      'permissionNameEn': 'Add task',
      'permissionKey': 'addTask'
    },
    {
      'id': 3,
      'permissionNameAr': 'اضافة مستخدم',
      'permissionNameEn': 'Add agent',
      'permissionKey': 'addAgent'
    },
    {
      'id': 4,
      'permissionNameAr': 'الوصول للاعدادات',
      'permissionNameEn': 'View settings',
      'permissionKey': 'viewSettings'
    },
    {
      'id': 5,
      'permissionNameAr': 'الوصول لإدارة المهمات',
      'permissionNameEn': 'View task management',
      'permissionKey': 'viewTaskManagement'
    },
    {
      'id': 6,
      'permissionNameAr': 'الوصول للداشبورد',
      'permissionNameEn': 'View dashboard',
      'permissionKey': 'viewDashboard'
    },
    {
      'id': 7,
      'permissionNameAr': 'تعديل المهمات',
      'permissionNameEn': 'Edit task',
      'permissionKey': 'editTask'
    },
    {
      'id': 8,
      'permissionNameAr': 'اضافة مستخدم',
      'permissionNameEn': 'Add agent',
      'permissionKey': 'addAgent'
    },
    {
      'id': 9,
      'permissionNameAr': 'اضافة صلاحية',
      'permissionNameEn': 'Add permission',
      'permissionKey': 'addPermission'
    },
    {
      'id': 10,
      'permissionNameAr': 'اضافة دور',
      'permissionNameEn': 'Add role',
      'permissionKey': 'addRole'
    },
    {
      'id': 11,
      'permissionNameAr': 'اضافة تمبلت',
      'permissionNameEn': 'add template',
      'permissionKey': 'addTemplate'
    }
  ];

  constructor(
    private settingsCrud: SettingsCrudsService,
    private route: ActivatedRoute,
    private router: Router
  ) {
  }

  ngOnInit() {
    this.routerSubscription = this.route.params.subscribe(r => {
      if (!r.rolesId) {
        this.createMode = true;
        this.roles = new RoleDTO();

      } else {
        this.createMode = false;
        this.settingsCrud.getDTOById(this.controller, +r.rolesId).subscribe(Roles => {
          this.roles = Roles;
        });
      }
    });
  }

  updateRoles() {
    this.settingsCrud.updateDTO(this.controller, [this.roles]).subscribe(result => {
      if (result) {
        this.router.navigate(['/settings/Roles']);
      }
    });
  }

  insertRoles() {
    this.settingsCrud.insertDTO(this.controller, [this.roles]).subscribe(result => {
      if (result) {
        this.router.navigate(['/settings/Roles']);
      }
    });
  }

  getPermissionsLookup() {
    this.settingsCrud.getAll('Permissions', 10, 0);
  }

  selectPermission(event) {
    console.log(event, 'permissions changed');
    console.log(this.permissionIds, 'permissions arr');
  }

}
