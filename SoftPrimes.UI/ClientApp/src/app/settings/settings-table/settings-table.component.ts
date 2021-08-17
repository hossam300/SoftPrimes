import { SettingsCrudsService } from './../settings-cruds.service';
import { Component, Input, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-settings-table',
  templateUrl: './settings-table.component.html',
  styleUrls: ['./settings-table.component.css']
})
export class SettingsTableComponent implements OnInit {
  @Input() data: any[];
  @Input() options: any;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private settingsCrud: SettingsCrudsService
  ) { }

  ngOnInit() {
  }

  editRecord(id) {
    this.router.navigate(['edit/' + id], {relativeTo: this.activatedRoute});
  }

  deleteRecord(id) {
    this.settingsCrud.deleteDTO(this.options.controller, id).subscribe(result => {
      if (result) {
        this.data = this.data.filter(record => record.id !== id);
      }
    });
  }

}
