import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TaskManagementRoutingModule } from './task-management-routing.module';
import { TasksListComponent } from './tasks-list/tasks-list.component';
import { NewTaskComponent } from './new-task/new-task.component';
import { TaskManagementWrapperComponent } from './task-management-wrapper/task-management-wrapper.component';


@NgModule({
  declarations: [TasksListComponent, NewTaskComponent, TaskManagementWrapperComponent],
  imports: [
    CommonModule,
    TaskManagementRoutingModule,
    SharedModule
  ]
})
export class TaskManagementModule { }
