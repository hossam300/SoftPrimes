import { SettingsCrudsService } from './settings/settings-cruds.service';
import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InjectionToken, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { TranslateModule } from '@ngx-translate/core';
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { TaskManagementModule } from './task-management/task-management.module';
import { NgApexchartsModule } from "ng-apexcharts";

import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { LoginComponent } from './auth/login/login.component';

import { API_BASE_URL, SwaggerClient } from './core/_services/swagger/SwaggerClient.service';
import { InterceptorService } from './core/_services/swagger/interceptor.service';
import { EnvServiceFactory } from './core/_services/env.service.provider';
import { environment } from 'src/environments/environment';
import { AuthGuard } from './core/_guards/auth.guard';
import { AuthService } from './core/_services/auth.service';
import { TaskManagementService } from './core/_services/task-management.service';
import { ProfileComponent } from './components/profile/profile.component';
import { LineChartComponent } from './components/line-chart/line-chart.component';
import { PieChartComponent } from './components/pie-chart/pie-chart.component';
export const DEV_MODE = new InjectionToken<boolean>('DEV_MODE');

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    DashboardComponent,
    ProfileComponent,
    LoginComponent,
    LineChartComponent,
    PieChartComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    TaskManagementModule,
    TranslateModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-left',
      closeButton: true,
      maxOpened: 5
    }),
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    NgApexchartsModule
  ],
  providers: [
    SwaggerClient,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorService,
      multi: true
    }, {
      provide: API_BASE_URL,
      useValue: EnvServiceFactory().apiUrl
    },
    {
      provide: DEV_MODE,
      useValue: !environment.production
    },
    Title,
    AuthGuard,
    AuthService,
    TaskManagementService,
    SettingsCrudsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
