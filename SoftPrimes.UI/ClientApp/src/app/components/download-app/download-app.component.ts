import { Component, OnInit } from '@angular/core';
import { EnvService } from 'src/app/core/_services/api-address.service';

@Component({
  selector: 'app-download-app',
  templateUrl: './download-app.component.html',
  styleUrls: ['./download-app.component.css']
})
export class DownloadAppComponent implements OnInit {

  iosUrl = 'https://www.apple.com/app-store/';
  androidUrl = 'https://play.google.com/store';

  constructor(private env: EnvService) {
    this.iosUrl = this.env.iosUrl;
    this.androidUrl = this.env.androidUrl;
  }

  ngOnInit() {
  }

}
