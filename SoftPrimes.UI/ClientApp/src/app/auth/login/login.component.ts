import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SwaggerClient, UserLoginModel } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { AuthService } from 'src/app/core/_services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  isAdmin = false;
  existingUser;
  returnUrl: string;

  dataError = false;
  error = 'An error occurred';
  request = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private auth: AuthService,
    private swagger: SwaggerClient) {}

  ngOnInit() {
    this.isAdmin = localStorage.getItem('isAdmin') ? JSON.parse(localStorage.getItem('isAdmin')) : false;
    this.initForms();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  initForms() {
    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  onSubmit() {
    let credentials = new UserLoginModel();

    credentials = <UserLoginModel>{
      username: this.loginForm.controls.username
        ? this.loginForm.controls.username.value
        : '',
      password: this.loginForm.controls.password
        ? this.loginForm.controls.password.value
        : '',
    };
    // login with diffrent states in any browser
    this.auth.login(credentials, localStorage['culture'] || 'en').subscribe(
      (isLoggedIn) => {
        this.isAdmin = this.auth.isAdmin;
        if (isLoggedIn) {
          this.getUserAuthData();
        }
      },
      (error: HttpErrorResponse) => {
        this.request = false;
        this.dataError = true;
        if (error.status === 401) {
          this.error = 'Invalid User name or Password. Please try again.';
        } else {
          this.error = `${error.statusText}: ${error.message}`;
        }
        // this.layoutService.toggleIsLoadingBlockUI(false);
      },
      () => {
        this.request = false;
      }
    );
  }

  getUserAuthData() {
    this.swagger
      .apiAccountGetUserAuthTicketGet(undefined, undefined, undefined)
      .subscribe((value) => {
        if (value) {
          localStorage.setItem('existing-user', JSON.stringify({
            'username': this.loginForm.controls.username.value,
            'nameEn': value.fullNameEn,
            'nameAr': value.fullNameAr,
            'email': value.email
            // 'image': value.userImage
          }));
          this.existingUser = JSON.parse(localStorage.getItem('existing-user'));
          this.auth.setUser(value);
          this.router.navigate([this.returnUrl]);
        }
        console.log(value, 'user get');
        // this.layoutService.toggleIsLoadingBlockUI(false);
      });
  }
}
