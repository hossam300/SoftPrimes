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
    console.log(newBreadcrumb, 'breadcrumbs from routing');
    const mappedBreadcrumbs = newBreadcrumb.map((x, i) => {
      return {
        label: this.generateLabel(x),
        url: this.generateUrl(newBreadcrumb, i)
      };
    });
    this.breadcrumbs = [...this.breadcrumbs, ...mappedBreadcrumbs];
    console.log(this.breadcrumbs, 'final bread crumbs');
  }

  generateLabel(string: string) {
    if (string.includes('-')) {
      const arr = string.split('-');
      const lastWord = arr[arr.length - 1].charAt(0).toUpperCase() + arr[arr.length - 1].slice(1);
      arr[arr.length - 1] = lastWord;
      return arr.join('');
    }
    return string;
  }

  generateUrl(arr, index) {
    let url = '';
    for (let i = 0; i < arr.length; i++) {
        if (i <= index) {
            url += '/' + arr[i];
        }
    }
    return url;
  }

}
