import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

export interface BreadCrumb {
  label: string;
  url: string;
}

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})
export class BreadcrumbComponent implements OnInit {
  breadcrumbs: BreadCrumb[] = [
    {
      label: 'Home',
      url: '/'
    }
  ];

  constructor(
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.buildBreadcrumb();
  }

  buildBreadcrumb() {
    const newBreadcrumb = this.route.routeConfig.data['breadcrumb'];
    const mappedBreadcrumbs = newBreadcrumb.map((x, i) => {
      return {
        label: x,
        url: '../'.repeat(newBreadcrumb.length - (i + 1))
      };
    });
    this.breadcrumbs = [...this.breadcrumbs, ...mappedBreadcrumbs];
    console.log(this.route.routeConfig, 'routeConfig');
    console.log(this.breadcrumbs);

  }

}
