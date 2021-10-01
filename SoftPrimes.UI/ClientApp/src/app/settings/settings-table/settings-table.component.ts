import { SettingsCrudsService } from './../settings-cruds.service';
import { Component, EventEmitter, Input, OnInit, Output, OnChanges } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
import { LoaderService } from 'src/app/core/_services/loader.service';

export enum AgentType {
  NormalAgent = 1,
  Supervisor = 2,
  Admin = 3
}

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
  closeResult = '';
  activeQrCode;
  activeRow;
  activeCol;
  agentType = AgentType;
  isArabic = false;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private settingsCrud: SettingsCrudsService,
    private modalService: NgbModal,
  ) {
    this.isArabic = localStorage.getItem('culture') === 'ar' ? true : false;
  }

  editRecord(id) {
    this.router.navigate(['edit/' + id], {relativeTo: this.activatedRoute});
  }

  deleteRecord(id) {
    // this.loader.addLoader();
    this.settingsCrud.deleteDTO(this.options.controller, id).subscribe(result => {
      // this.loader.removeLoader();;
      if (result) {
        this.data = this.data.filter(record => record.id !== id);
      }
    });
  }

  toggleTemplate(id, active) {
    // this.loader.addLoader();
    this.settingsCrud.toggleTemplate(id, !active).subscribe(result => {
      // this.loader.removeLoader();;
      if (result) {
        this.data = this.data.map(x => {
          if (x.id === id) {
            x.active = !active;
          }
          return x;
        });
      }
    });
  }

  emitPagination() {
    const skipVal = (this.currentPage - 1) * this.pageSize;
    this.skip.emit(skipVal);
  }

  open(content, rowData, colName) {
    this.activeQrCode = rowData.toString();
    this.activeRow = rowData;
    this.activeCol = colName;
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  printDiv(divName) {
    this.settingsCrud.printQr(divName);
  }

}
