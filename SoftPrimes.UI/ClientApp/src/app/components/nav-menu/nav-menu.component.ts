import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { LocalizationService } from 'src/app/core/_services/localization.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isLoggedIn;
  user: any;
  isArabic;

  constructor(
    private router: Router,
    private auth: AuthService,
    private localization: LocalizationService
  ) {}

  ngOnInit() {
    this.user = this.auth.currentUser;
    this.isArabic = this.auth.isArabic;
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

  changeLocal() {
    this.localization.changeLocal();
  }

  logout() {
    this.auth.logout();
  }
}
