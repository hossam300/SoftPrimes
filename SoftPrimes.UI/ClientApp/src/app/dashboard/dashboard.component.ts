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
  toursMonitoringChartOptions;

  checkPointsChartData = [];
  checkPointsChartOptions;

  overDueChartData = [];
  overDueChartOptions;

  agentDistanceChartData = [];
  agentDistanceChartOptions;

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
    const tours = [];
    const monitoring = [];
    this.dashboard.getTourMontringVsDate(null, null).subscribe(res => {
      this.toursMonitoringChartOptions.categories = [];
      res.montringVsDate.forEach(element => {
        monitoring.push(element.value);
        this.toursMonitoringChartOptions.categories.push(formatDateCorrectly(element.date));
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
      title: 'Tours and Monitoring overview',
      chartType: 'line',
      xaxisType: 'datetime',
      themePalette: 'palette2',
      labelXFormatter: function(val , timestamp) {
        const d = `${(new Date(val).getMonth() + 1)}/${new Date(val).getFullYear()}`;
        return d;
      }
    };
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
    const checkPointsX = [];
    const checkPointsY = [];
    this.dashboard.getCheckPointCount(null, null).subscribe(res => {
      console.log(res, 'checkpoint count');
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
      title: 'CheckPoints overview',
      chartType: 'line',
      xaxisType: 'category',
      themePalette: 'palette6',
      labelXFormatter: undefined
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
