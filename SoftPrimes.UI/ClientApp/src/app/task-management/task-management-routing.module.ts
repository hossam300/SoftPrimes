import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TaskManagementWrapperComponent } from './task-management-wrapper/task-management-wrapper.component';
import { TasksListComponent } from './tasks-list/tasks-list.component';
import { NewTaskComponent } from './new-task/new-task.component';


const routes: Routes = [{
  path: '', component: TaskManagementWrapperComponent,
  children: [
    { path: '', redirectTo: 'tasks-list', pathMatch: 'full' },
    { path: 'tasks-list', component: TasksListComponent },
    { path: 'new-task', component: NewTaskComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TaskManagementRoutingModule { }
