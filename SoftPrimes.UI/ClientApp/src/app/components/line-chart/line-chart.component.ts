import { Component, Input, OnInit, ViewChild, OnChanges } from '@angular/core';

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
  ApexPlotOptions,
  ChartComponent,
  ApexNoData
} from 'ng-apexcharts';

export interface ChartOptions {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  markers: ApexMarkers;
  title: ApexTitleSubtitle;
  fill: ApexFill;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  tooltip: ApexTooltip;
  legend: ApexLegend;
  theme: ApexTheme;
  stroke: ApexStroke;
  plotOptions: ApexPlotOptions;
  noData: ApexNoData;
}

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.css']
})
export class LineChartComponent implements OnInit, OnChanges {
  public options: Partial<ChartOptions>;
  @Input() dataSeries = [];
  @Input() chartOptions;
  @ViewChild('lineChart', {read: false, static: false}) lineChart: ChartComponent;

  constructor() { }

  ngOnChanges() {
    if (this.lineChart && this.chartOptions.categories) {
      this.options.xaxis.categories = this.chartOptions.categories;
      this.options.series = this.dataSeries;
      this.lineChart.updateOptions(this.options, true, true, true);
    }
  }

  ngOnInit() {
    this.initChartData();
  }

  public initChartData(): void {
    this.options = {

      series: this.dataSeries,
      chart: {
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
      },
      plotOptions: {
        bar: {
          horizontal: false
        }
      },
      dataLabels: {
        enabled: false
      },
      legend: {
        show: true,
        position: 'top',
        horizontalAlign: 'center'
      },
      theme: {
        palette: this.chartOptions.themePalette || ('palette' + Math.floor(Math.random() * 10)),
        monochrome: {
          enabled: false,
          color: '#255aee',
          shadeTo: 'light',
          shadeIntensity: 0.65
        },
      },
      stroke: {
        show: true,
        lineCap: 'round',
        curve: 'smooth',
        width: 2
      },
      markers: {
        size: 5
      },
      title: {
        text: this.chartOptions.title,
        align: 'left',
        style: {
          fontWeight: 'normal',
          fontSize: '20px'
        }
      },
      fill: {
        type: 'solid',
        gradient: {
          shadeIntensity: 1,
            inverseColors: true,
            opacityFrom: 1,
            opacityTo: 0,
            stops: [0, 90, 100]
          }
        },
        yaxis: {
          labels: {
            formatter: function (val) {
              if (val > 1000) {
                return (val / 1000000).toFixed(0);
              } else {
                return val.toFixed(0);
              }
            }
          }
      },
      xaxis: {
        type: this.chartOptions.xaxisType,
        categories: this.chartOptions.categories || []
      },
      tooltip: {
        shared: false,
        y: {
          formatter: function (val) {
            if (val > 1000) {
              return (val / 1000000).toFixed(0);
            } else {
              return val.toFixed(0);
            }
          }
        },
        x: {
          formatter: this.chartOptions.labelXFormatter
        }
      },
    };
  }

}
