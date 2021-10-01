import { Component, Input, OnInit } from '@angular/core';

import {
  ApexAxisChartSeries,
  ApexChart,
  ApexTitleSubtitle,
  ApexDataLabels,
  ApexFill,
  ApexMarkers,
  ApexYAxis,
  ApexXAxis,
  ApexTooltip,
  ApexLegend,
  ApexTheme,
  ApexStroke,
  ApexPlotOptions
} from 'ng-apexcharts';

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.css']
})
export class LineChartComponent implements OnInit {
  public series: ApexAxisChartSeries;
  public chart: ApexChart;
  public dataLabels: ApexDataLabels;
  public markers: ApexMarkers;
  public title: ApexTitleSubtitle;
  public fill: ApexFill;
  public yaxis: ApexYAxis;
  public xaxis: ApexXAxis;
  public tooltip: ApexTooltip;
  public legend: ApexLegend;
  public theme: ApexTheme;
  public stroke: ApexStroke;
  public plotOptions: ApexPlotOptions;

  @Input() dataSeries = [];
  @Input() chartOptions;

  constructor() { }

  ngOnInit() {
    this.initChartData();
  }

  public initChartData(): void {
    this.series = this.dataSeries;
    this.chart = {
      type: this.chartOptions.chartType,
      stacked: false,
      height: 350,
      zoom: {
        type: 'x',
        enabled: true,
        autoScaleYaxis: true
      },
      toolbar: {
        autoSelected: 'zoom'
      }
    };
    this.plotOptions = {
      bar: {
        horizontal: false
      }
    };
    this.dataLabels = {
      enabled: false
    };
    this.legend = {
      show: true,
      position: 'top',
      horizontalAlign: 'center'
    };
    this.theme = {
      palette: this.chartOptions.themePalette || ('palette' + Math.floor(Math.random() * 10)),
      monochrome: {
        enabled: false,
        color: '#255aee',
        shadeTo: 'light',
        shadeIntensity: 0.65
      },
    };
    this.stroke = {
      show: true,
      lineCap: 'round',
      curve: 'smooth',
      width: 1
    };
    this.markers = {
      size: 5
    };
    this.title = {
      text: this.chartOptions.title,
      align: 'left',
      style: {
        fontWeight: 'normal',
        fontSize: '20px'
      }
    };
    this.fill = {
      type: 'solid',
      gradient: {
        shadeIntensity: 1,
        inverseColors: true,
        opacityFrom: 1,
        opacityTo: 0,
        stops: [0, 90, 100]
      }
    };
    this.yaxis = {
      labels: {
        formatter: function (val) {
          if (val > 1000) {
            return (val / 1000000).toFixed(0);
          } else {
            return val.toFixed(0);
          }
        }
      }
    };
    this.xaxis = {
      type: this.chartOptions.xaxisType,
      categories: this.chartOptions.categories || []
    };
    this.tooltip = {
      shared: false,
      y: {
        formatter: function (val) {
          if (val > 1000) {
            return (val / 1000000).toFixed(0);
          } else {
            return val.toFixed(0);
          }
        }
      }
    };
  }

}
