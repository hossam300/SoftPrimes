import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TaskManagementWrapperComponent } from './task-management-wrapper/task-management-wrapper.component';
import { TasksListComponent } from './tasks-list/tasks-list.component';
import { NewTaskComponent } from './new-task/new-task.component';

import { AuthGuard } from '../core/_guards/auth.guard';


const routes: Routes = [{
  path: '', component: TaskManagementWrapperComponent,
  canActivate: [AuthGuard], canActivateChild: [AuthGuard],
  children: [
    { path: '', redirectTo: 'tasks-list', pathMatch: 'full', data: {'breadcrumb': ['taskManagment']} },
    { path: 'tasks-list', component: TasksListComponent, data: {'breadcrumb': ['taskManagment']} },
    { path: 'new-task', component: NewTaskComponent, data: {'breadcrumb': ['taskManagment', 'newTask']} },
    { path: '**', redirectTo: 'tasks-list', pathMatch: 'full' },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TaskManagementRoutingModule { }
