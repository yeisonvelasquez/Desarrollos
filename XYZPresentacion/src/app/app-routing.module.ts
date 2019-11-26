import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//Components
import { ActivitiesComponent } from './core/activities/activities.component';
import { LoginComponent } from './core/login/login.component';
import { TimexactivityComponent } from './core/timexactivity/timexactivity/timexactivity.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'activities', component: ActivitiesComponent },
  { path: 'timexactivity', component: TimexactivityComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
