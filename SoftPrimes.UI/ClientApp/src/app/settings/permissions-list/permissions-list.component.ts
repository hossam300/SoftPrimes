import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-permissions-list',
  templateUrl: './permissions-list.component.html',
  styleUrls: ['./permissions-list.component.css']
})
export class PermissionsListComponent implements OnInit {
  permissionsList: any[];
  columns: string[];

  constructor() { }

  ngOnInit() {
    this.initTableColumns();
  }

  initTableColumns() {
    this.columns = [
      'agentName',
      'permissionKey',
      'creationDate',
      'comment',
      ''
    ];
  }

}
