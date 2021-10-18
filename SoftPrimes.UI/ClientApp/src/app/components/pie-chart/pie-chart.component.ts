import { Component, Input, OnInit, ViewChild, OnChanges } from '@angular/core';

import {
  ApexChart,
  ApexNoData,
  ApexNonAxisChartSeries,
  ApexPlotOptions,
  ApexResponsive,
  ApexTitleSubtitle,
  ChartComponent
} from 'ng-apexcharts';

export interface ChartOptions {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  title: ApexTitleSubtitle;
  plotOptions: ApexPlotOptions;
  responsive: ApexResponsive[];
  labels: any;
  noData: ApexNoData;
}

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent implements OnInit, OnChanges {
  public chartOptions: Partial<ChartOptions>;
  @Input() dataSeries = [];
  @Input() options;

  @ViewChild('chart', {read: false, static: false}) chart: ChartComponent;

  constructor() { }

  ngOnChanges() {
    if (this.chart && this.options.labels) {
      this.chartOptions.labels = this.options.labels;
      this.chartOptions.series = this.dataSeries;
      this.chart.updateOptions(this.chartOptions, true, true, true);
    }
  }

  ngOnInit() {
    this.chartOptions = {
      series: this.dataSeries,
      chart: {
        type: this.options.type
      },
      plotOptions: {
        pie: {
          donut: {
            size: this.options.size
          }
        }
      },
      title: {
        text: this.options.title
      },
      labels: this.options.labels,
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
      ],
      noData: {
        text: 'Loading...'
      }
    };
  }

}
