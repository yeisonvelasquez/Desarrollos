import { ActivitiesComponent } from './activities/activities.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';

const routes: Routes = [{
    path: '',
    component: LoginComponent,
    children:[
      {
          path: '',
          redirectTo: 'login'
      },
      {
          path: 'activities',
          component: ActivitiesComponent
      }
  ]
  }];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class CoreRoutingModule { } 