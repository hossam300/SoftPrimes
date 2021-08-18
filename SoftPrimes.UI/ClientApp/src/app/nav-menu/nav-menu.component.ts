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

  constructor(
    private router: Router,
    private auth: AuthService
  ) {}

  ngOnInit() {
    this.router.events.subscribe(val => {
      this.isLoggedIn = this.auth.isAuthUserLoggedIn();
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.auth.logout().subscribe(res => {
      if (res) {
        this.router.navigate(['/login']);
      }
    });
  }
}
