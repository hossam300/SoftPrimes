import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-permissions',
  templateUrl: './permissions.component.html',
  styleUrls: ['./permissions.component.css']
})
export class PermissionsComponent implements OnInit {
  permissionKey: string;
  permissionNameAr: string;
  permissionNameEn: string;

  constructor() { }

  ngOnInit() {
  }

}
