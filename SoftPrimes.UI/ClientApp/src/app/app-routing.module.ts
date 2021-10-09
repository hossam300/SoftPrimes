import { ProfileComponent } from './components/profile/profile.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './core/_guards/auth.guard';

import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './auth/login/login.component';


const routes: Routes = [
  { path: '', component: DashboardComponent, canActivate: [AuthGuard],
    data: {'breadcrumb': []}, pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'profile', component: ProfileComponent,
    data: {'breadcrumb': ['profile']},
    children: [
      { path: 'edit/:userId', component: ProfileComponent, data: {'breadcrumb': ['profile', 'edit']} }
    ]
  },
  { path: 'task-management',
  loadChildren: './task-management/task-management.module#TaskManagementModule',
  data: {'breadcrumb': ['taskManagment']} },
  { path: 'settings',
  loadChildren: './settings/settings.module#SettingsModule',
  data: {'breadcrumb': ['settings']} },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
