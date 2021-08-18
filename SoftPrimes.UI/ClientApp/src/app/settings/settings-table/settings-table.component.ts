import { SettingsCrudsService } from './../settings-cruds.service';
import { Component, EventEmitter, Input, OnInit, Output, OnChanges } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-settings-table',
  templateUrl: './settings-table.component.html',
  styleUrls: ['./settings-table.component.css']
})
export class SettingsTableComponent {
  @Input() data: any[];
  @Input() options: any;
  @Input() pageSize = 5;
  @Input() count;
  @Output() skip = new EventEmitter<number>();
  currentPage = 1;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private settingsCrud: SettingsCrudsService
  ) { }

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

  emitPagination() {
    const skipVal = (this.currentPage - 1) * this.pageSize;
    this.skip.emit(skipVal);
  }

}
