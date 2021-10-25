import { DashboardService } from './../core/_services/dashboard.service';
import { Component, OnInit } from '@angular/core';
import { dataSeries } from '../core/_utils/data-series';
import { PiChartDTO } from '../core/_services/swagger/SwaggerClient.service';
import { formatDateCorrectly } from '../core/_utils/date';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  chartData = [];
  toursMonitoringChartOptions: any = {};

  checkPointsChartData = [];
  checkPointsChartOptions: any = {};

  overDueChartData = [];
  overDueChartOptions: any = {};

  agentDistanceChartData = [];
  agentDistanceChartOptions: any = {};

  tourStatusChartData = [];
  tourStatusChartOptions: any = {};
  toDate = new Date();

  timePeriod = [7, 15, 30, 60, 90];
  fromVal;

  constructor(private dashboard: DashboardService) {}

  ngOnInit() {
    this.initToursMonitoringChart(null, null);
    this.initOverDueChart(null, null);
    this.initCheckPointsChart(null, null);
    this.initAgentDistaceChart(null, null);
    this.initTourStatusChart(null, null);
  }

  getData(val , chartType) {
    const date = this.convertDaysToDate(val);
    switch (chartType) {
      case 'tourStatusChart':
        this.initTourStatusChart(date, this.toDate);
        break;
      case 'overDueChart':
        this.initOverDueChart(date, this.toDate);
        break;
      case 'toursMonitoringChart':
        this.initToursMonitoringChart(date, this.toDate);
        break;
      case 'checkPointsChart':
        this.initCheckPointsChart(date, this.toDate);
        break;
      default:
        this.initAgentDistaceChart(date, this.toDate);
        break;
    }
  }

  convertDaysToDate(val: number) {
    const date = new Date();
    date.setDate(date.getDate() - val);
    return date;
  }

  initToursMonitoringChart(fromDate, toDate) {
    const tours = [];
    const monitoring = [];
    this.dashboard.getTourMontringVsDate(fromDate, toDate).subscribe(res => {
      this.toursMonitoringChartOptions.categories = [];
      res.montringVsDate.forEach(element => {
        monitoring.push(element.value);
        this.toursMonitoringChartOptions.categories.push(element.date);
      });
      res.tourVsDate.forEach(element => {
        tours.push(element.value);
      });
      const data = [
        {
          name: 'Tours',
          data: tours
        },
        {
          name: 'Monitoring',
          data: monitoring
        }
      ];
      this.chartData = data;
    });
    this.toursMonitoringChartOptions = {
      title: '',
      chartType: 'line',
      xaxisType: 'datetime',
      themePalette: 'palette2',
      labelXFormatter: function(val , timestamp) {
        const d = `${(new Date(val).getDate())}/${(new Date(val).getMonth() + 1)}/${new Date(val).getFullYear()}`;
        return d;
      }
    };
  }

  initOverDueChart(fromDate, toDate) {
    this.dashboard.getToursOverDueData(fromDate, toDate).subscribe(res => {
      if (res) {
        const overDue = res.map(x => x.value);
        const dates = res.map(x => x.date);
        this.overDueChartData = [
          {
            name: 'over due tours',
            data: overDue
          }
        ];

        this.overDueChartOptions.categories = dates;
      }
    });
    this.overDueChartOptions = {
      title: '',
      chartType: 'line',
      xaxisType: 'datetime',
      themePalette: 'palette2',
      labelXFormatter: function(val , timestamp) {
        const d = `${(new Date(val).getDate())}/${(new Date(val).getMonth() + 1)}/${new Date(val).getFullYear()}`;
        return d;
      }
    };
  }

  initCheckPointsChart(fromDate, toDate) {
    const checkPointsX = [];
    const checkPointsY = [];
    this.dashboard.getCheckPointCount(fromDate, toDate).subscribe(res => {
      res.forEach(el => {
        checkPointsY.push(el.value);
        checkPointsX.push(el['text']);
      });

      this.checkPointsChartOptions.categories = checkPointsX;

      const data = [
        {
          name: 'CheckPoints',
          data: checkPointsY
        }
      ];

      this.checkPointsChartData = data;
    });

    this.checkPointsChartOptions = {
      title: '',
      chartType: 'line',
      xaxisType: 'category',
      themePalette: 'palette6',
      labelXFormatter: undefined
    };
  }

  initAgentDistaceChart(fromDate, toDate) {
    let agentDistanceY, agentDistanceX;
    this.dashboard.getAgentDistanceData(fromDate, toDate).subscribe(res => {
      this.agentDistanceChartOptions.categories = [];
      if (res) {
        agentDistanceY = res.map(x => x.value);
        agentDistanceX = res.map(x => x.text);
      }
      this.agentDistanceChartData = [
        {
          name: 'Distance',
          data: agentDistanceY
        }
      ];

      this.agentDistanceChartOptions.categories = agentDistanceX;
    });
    this.agentDistanceChartOptions = {
      title: '',
      chartType: 'bar',
      xaxisType: 'category',
      themePalette: 'palette4'
    };
  }

  initTourStatusChart(fromDate, toDate) {
    let keys;
    this.dashboard.getTourStatusData(fromDate, toDate).subscribe(res => {
      const values = res.map(item => item.value);
      keys = res.map(item => item.text);
      this.tourStatusChartData = values;
      this.tourStatusChartOptions.labels = keys;
    });
    this.tourStatusChartOptions = {
      type: 'donut',
      size: '60%',
      title: '',
    };
  }

}
