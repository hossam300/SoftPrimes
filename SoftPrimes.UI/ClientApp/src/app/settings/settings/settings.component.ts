import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  settings = [
    {
      name: 'Roles',
      url: 'roles'
    },
    {
      name: 'Permissions',
      url: 'permissions'
    },
    {
      name: 'Templates',
      url: 'templates'
    },
    {
      name: 'Agents',
      url: 'agents'
    },
    {
      name: 'Localization',
      url: 'localization'
    },
    {
      name: 'Change Language',
      url: 'language'
    },
    {
      name: 'Download App',
      url: 'download'
    },
  ];

  constructor() { }

  ngOnInit() {
  }

}
