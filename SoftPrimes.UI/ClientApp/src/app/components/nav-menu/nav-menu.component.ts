import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isLoggedIn;
  user: any;

  constructor(
    private router: Router,
    private auth: AuthService
  ) {}

  ngOnInit() {
    this.user = this.auth.currentUser;
    this.router.events.subscribe(val => {
      this.isLoggedIn = this.auth.isAuthUserLoggedIn();
    });
  }

  handleImage(url: string) {
    if (url && !url.includes('base64')) {
      url = 'data:image/jpeg;base64,' + url;
    }
    return url;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.auth.logout();
  }
}
