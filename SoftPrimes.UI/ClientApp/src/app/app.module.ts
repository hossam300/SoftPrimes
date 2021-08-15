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

import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './auth/login/login.component';

import { API_BASE_URL, SwaggerClient } from './core/_services/swagger/SwaggerClient.service';
import { InterceptorService } from './core/_services/swagger/interceptor.service';
import { EnvServiceFactory } from './core/_services/env.service.provider';
import { environment } from 'src/environments/environment';
import { AuthGuard } from './core/_guards/auth.guard';
import { AuthService } from './core/_services/auth.service';
export const DEV_MODE = new InjectionToken<boolean>('DEV_MODE');

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    DashboardComponent,
    LoginComponent
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
    SharedModule
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
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
