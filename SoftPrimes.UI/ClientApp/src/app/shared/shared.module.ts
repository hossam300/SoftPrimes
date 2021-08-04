import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { GmapComponent } from './gmap/gmap.component';
import { DataTableComponent } from './data-table/data-table.component';



@NgModule({
  declarations: [BreadcrumbComponent, GmapComponent, DataTableComponent],
  imports: [
    CommonModule
  ],
  exports: [BreadcrumbComponent, GmapComponent, DataTableComponent]
})
export class SharedModule { }
