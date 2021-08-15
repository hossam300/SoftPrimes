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
    private auth: AuthService
  ){}

  ngOnInit() {
    this.isLoggedIn = this.auth.currentUser;
    console.log(this.isLoggedIn, 'user logged in');

  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
