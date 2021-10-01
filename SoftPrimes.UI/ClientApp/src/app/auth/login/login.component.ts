import { LoaderService } from './../../core/_services/loader.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SwaggerClient, UserLoginModel } from 'src/app/core/_services/swagger/SwaggerClient.service';
import { AuthService } from 'src/app/core/_services/auth.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

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
  forgetPass = false;
  isTempPass = false;

  dataError = false;
  error = 'An error occurred';
  request = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private auth: AuthService,
    private toastr: ToastrService,
    private swagger: SwaggerClient) {}

  ngOnInit() {
    this.isAdmin = localStorage.getItem('isAdmin') ? JSON.parse(localStorage.getItem('isAdmin')) : false;
    this.initLoginForms();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  initLoginForms() {
    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  initResetForms() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  initTempPassForms() {
    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required,
        Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]],
      confirm_password: ['', [Validators.required,
        Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]],
    });
  }

  forgetPassword() {
    this.forgetPass = !this.forgetPass;
    if (this.forgetPass) {
      this.initResetForms();
    } else {
      this.initLoginForms();
    }
  }

  getTempPass() {
    this.swagger.apiAccountResetPasswordGet(this.loginForm.controls.email.value).subscribe(res => {
      if (res) {
        this.initLoginForms();
        this.forgetPass = false;
        this.toastr.info('Please check your email', 'Reset Password');
      }
    });
  }

  ResetPassword() {
    this.swagger.apiAccountChangeTempPasswordGet(
      this.loginForm.controls.username.value,
      this.loginForm.controls.password.value
      ).
      subscribe(res => {
        if (res) {
          this.initLoginForms();
          this.isTempPass = false;
          this.forgetPass = false;
        }
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
    // this.loader.addLoader();
    // login with diffrent states in any browser
    this.auth.login(credentials, localStorage['culture']).subscribe(
      (isLoggedIn) => {
        if (isLoggedIn) {
          this.isAdmin = this.auth.isAdmin;
          if (this.auth.isTemp) {
            this.initTempPassForms();
            this.isTempPass = this.auth.isTemp;
          } else {
            this.getUserAuthData();
          }
        }
      },
      (error: HttpErrorResponse) => {
        // this.loader.removeLoader();;
        this.request = false;
        this.dataError = true;
        if (error.status === 401) {
          this.error = 'Invalid User name or Password. Please try again.';
        } else {
          this.error = `${error.statusText}: ${error.message}`;
        }
        this.toastr.error(this.error, 'Login Failed');
        // this.layoutService.toggleIsLoadingBlockUI(false);
      },
      () => {
        this.request = false;
      }
    );
  }

  getUserAuthData() {
    // this.loader.addLoader();
    this.swagger
      .apiAccountGetUserAuthTicketGet()
      .subscribe((value) => {
        // this.loader.removeLoader();;
        if (value) {
          // localStorage.setItem('existing-user', JSON.stringify({
          //   'username': this.loginForm.controls.username.value,
          //   'nameEn': value.fullNameEn,
          //   'nameAr': value.fullNameAr,
          //   'email': value.email
          //   // 'image': value.userImage
          // }));
          // this.existingUser = JSON.parse(localStorage.getItem('existing-user'));
          this.auth.setUser(value);
          this.router.navigate([this.returnUrl]);
        }
        console.log(value, 'user get');
        // this.layoutService.toggleIsLoadingBlockUI(false);
      });
  }
}
