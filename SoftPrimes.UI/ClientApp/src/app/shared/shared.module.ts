import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { GmapComponent } from './gmap/gmap.component';
import { DataTableComponent } from './data-table/data-table.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AgmCoreModule } from '@agm/core';
import { AvatarModule } from 'ngx-avatar';
import { AgmOverlays } from "agm-overlays"


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
      apiKey: 'AIzaSyBQ0sLTl03ZtdTMW5iUnP7kHXMCldTR2kM'
      // apiKey: 'AIzaSyAFRKYD119NSdHb39E4nkA3iXjoKtB0oks'
    }),
    AvatarModule
  ],
  exports: [BreadcrumbComponent, GmapComponent, DataTableComponent, NgbModule, AgmCoreModule, AgmOverlays, AvatarModule]
})
export class SharedModule { }