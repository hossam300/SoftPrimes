import { SettingsCrudsService } from './../settings-cruds.service';
import { Component, EventEmitter, Input, OnInit, Output, OnChanges } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';

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

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private settingsCrud: SettingsCrudsService,
    private modalService: NgbModal
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

  open(content, rowData, colName) {
    this.activeQrCode = rowData.toString();
    this.activeRow = rowData;
    this.activeCol = colName;
    console.log(this.activeQrCode, 'id is: ');
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
