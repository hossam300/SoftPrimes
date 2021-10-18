import { SwaggerClient } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private swagger: SwaggerClient) { }

  getTourStatusData(startDate, endDate) {
    return this.swagger.apiDashboardTourStatusGet(startDate, endDate);
  }

  getCheckPointCount(startDate, endDate) {
    return this.swagger.apiDashboardCheckPointCountGet(startDate, endDate);
  }

  getTourMontringVsDate(startDate, endDate) {
    return this.swagger.apiDashboardTourMontringVsDateGet(startDate, endDate);
  }
}
