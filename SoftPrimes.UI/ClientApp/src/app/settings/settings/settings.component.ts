import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  settings = [
    {
      name: 'roles',
      url: 'roles'
    },
    {
      name: 'permissions',
      url: 'permissions'
    },
    {
      name: 'templates',
      url: 'templates'
    },
    {
      name: 'agents',
      url: 'agents'
    },
    {
      name: 'checkPoints',
      url: 'checkpoints'
    },
    {
      name: 'localization',
      url: 'localization'
    },
    {
      name: 'downloadApp',
      url: 'download'
    },
  ];

  constructor() { }

  ngOnInit() {
  }

}
