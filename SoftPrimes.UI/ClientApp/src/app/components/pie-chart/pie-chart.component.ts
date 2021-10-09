import { Component, OnInit } from '@angular/core';

import {
  ApexChart,
  ApexNonAxisChartSeries,
  ApexPlotOptions,
  ApexResponsive,
  ApexTitleSubtitle
} from 'ng-apexcharts';

export interface ChartOptions {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  title: ApexTitleSubtitle;
  plotOptions: ApexPlotOptions;
  responsive: ApexResponsive[];
  labels: any;
}

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements OnInit {
  public chartOptions: Partial<ChartOptions>;

  constructor() { }

  ngOnInit() {
    this.chartOptions = {
      series: [44, 55, 13, 43, 22],
      chart: {
        type: 'donut'
      },
      plotOptions: {
        pie: {
          donut: {
            size: '60%'
          }
        }
      },
      title: {
        text: 'Tour status overview'
      },
      labels: ['InProgress', 'Completed', 'New', 'Not completed', 'Canceled'],
      responsive: [
        {
          breakpoint: 760,
          options: {
            chart: {
              width: 360
            },
            legend: {
              position: 'bottom'
            }
          }
        },
        {
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              position: 'bottom'
            }
          }
        }
      ]
    };
  }

}
