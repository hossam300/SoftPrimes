import { DashboardService } from './../core/_services/dashboard.service';
import { Component, OnInit } from '@angular/core';
import { dataSeries } from '../core/_utils/data-series';
import { PiChartDTO } from '../core/_services/swagger/SwaggerClient.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  chartData = [];
  toursMonitoringChartOptions = {};

  checkPointsChartData = [];
  checkPointsChartOptions = {};

  overDueChartData = [];
  overDueChartOptions = {};

  agentDistanceChartData = [];
  agentDistanceChartOptions = {};

  tourStatusChartData = [];
  tourStatusChartOptions;

  constructor(private dashboard: DashboardService) {}

  ngOnInit() {
    this.initToursMonitoringChart();
    this.initOverDueChart();
    this.initCheckPointsChart();
    this.initAgentDistaceChart();
    this.initTourStatusChart();
  }

  initToursMonitoringChart() {
    this.toursMonitoringChartOptions = {
      title: 'Tours and Monitoring overview',
      chartType: 'line',
      xaxisType: 'datetime',
      themePalette: 'palette2'
    };
    let ts1 = 1484418600000;
    let ts2 = 1484418600000;
    const tours = [];
    const monitoring = [];
    for (let i = 0; i < 23; i++) {
      ts1 = ts1 + 7640000;
      ts2 = ts2 + 8640000;
      tours.push([ts2, dataSeries[1][i].value]);
      monitoring.push([ts1, dataSeries[2][i].value]);
    }

    this.chartData = [
      {
        name: 'Tours',
        data: tours
      },
      {
        name: 'Monitoring',
        data: monitoring
      }
    ];
  }

  initOverDueChart() {
    this.overDueChartOptions = {
      title: 'over due overview',
      chartType: 'line',
      xaxisType: 'datetime',
      themePalette: 'palette2'
    };
    let ts2 = 1484418600000;
    const overDue = [];
    for (let i = 0; i < 23; i++) {
      ts2 = ts2 + 8640000;
      overDue.push([ts2, dataSeries[1][i].value]);
    }

    this.overDueChartData = [
      {
        name: 'over due',
        data: overDue
      }
    ];
  }

  initCheckPointsChart() {
    let ts2 = 1484418600000;
    const checkPointsX = [];
    const checkPointsY = [];
    for (let i = 0; i < 23; i++) {
      ts2 = ts2 + 8640000;
      checkPointsY.push(dataSeries[3][i].value);
      checkPointsX.push(dataSeries[3][i]['name']);
    }

    this.checkPointsChartData = [
      {
        name: 'CheckPoints',
        data: checkPointsY
      }
    ];

    this.checkPointsChartOptions = {
      title: 'CheckPoints overview',
      chartType: 'line',
      xaxisType: 'category',
      themePalette: 'palette6',
      categories: checkPointsX
    };
  }

  initAgentDistaceChart() {
    let ts2 = 1484418600000;
    const agentDistanceX = [];
    const agentDistanceY = [];
    for (let i = 0; i < 23; i++) {
      ts2 = ts2 + 8640000;
      agentDistanceY.push(dataSeries[3][i].value);
      agentDistanceX.push(dataSeries[3][i]['name']);
    }

    this.agentDistanceChartData = [
      {
        name: 'Distance',
        data: agentDistanceY
      }
    ];

    this.agentDistanceChartOptions = {
      title: 'Agent distance overview',
      chartType: 'bar',
      xaxisType: 'category',
      themePalette: 'palette4',
      categories: agentDistanceX
    };
  }

  initTourStatusChart() {
    let keys;
    this.dashboard.getTourStatusData(null, null).subscribe(res => {
      const values = res.map(item => item.value);
      keys = res.map(item => item.text);
      this.tourStatusChartData = values;
      this.tourStatusChartOptions.labels = keys;
    });
    this.tourStatusChartOptions = {
      type: 'donut',
      size: '60%',
      title: 'Tour status overview',
    };
  }

}
