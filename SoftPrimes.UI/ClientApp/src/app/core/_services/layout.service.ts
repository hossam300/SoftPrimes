import { TranslateService } from '@ngx-translate/core';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';


@Injectable({
  providedIn: 'root'
})
export class LayoutService {

  constructor(
    private toastr: ToastrService,
    private translate: TranslateService
  ) {
  }

  handleSuccessMsg(req) {
    if (
      req &&
      ['put', 'post', 'delete'].indexOf(req.method.toLowerCase()) > -1
    ) {
      this.translate.get('successMsg').subscribe(x => {
        this.toastr.success('', x);
      });
    }
  }

  handleFailMsg(req) {
    if (
      req &&
      ['put', 'post', 'delete'].indexOf(req.method.toLowerCase()) > -1
    ) {
      this.translate.get('failMsg').subscribe(x => {
        this.toastr.error('', x);
      });
    }
  }
}
