import { environment } from 'src/environments/environment';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { GmapComponent } from './gmap/gmap.component';
import { DataTableComponent } from './data-table/data-table.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AgmCoreModule } from '@agm/core';
import { AvatarModule } from 'ngx-avatar';
import { AgmOverlays } from 'agm-overlays';
import { NgSelectModule } from '@ng-select/ng-select';
import { QrCodeModule } from 'ng-qrcode';


@NgModule({
  declarations: [BreadcrumbComponent, GmapComponent, DataTableComponent],
  imports: [
    CommonModule,
    RouterModule,
    NgbModule,
    AgmOverlays,
    AgmCoreModule.forRoot({
      // please get your own API key here:
      // https://developers.google.com/maps/documentation/javascript/get-api-key?hl=en
      apiKey: environment['google-api-key'],
      // apiKey: 'AIzaSyAFRKYD119NSdHb39E4nkA3iXjoKtB0oks'
    }),
    AvatarModule,
    NgSelectModule,
    QrCodeModule
  ],
  exports: [
    BreadcrumbComponent,
    GmapComponent,
    DataTableComponent,
    NgbModule,
    AgmCoreModule,
    AgmOverlays,
    AvatarModule,
    NgSelectModule,
    QrCodeModule
  ]
})
export class SharedModule { }
