import { CoreRoutingModule } from './core-routing.module';
import { logging } from 'protractor';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PerfectScrollbarModule, PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

import { ActivitiesComponent } from './activities/activities.component';
import { LoginComponent } from './login/login.component';
import { TimexactivityComponent } from './timexactivity/timexactivity/timexactivity.component';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    // suppressScrollX: true
  };

@NgModule({
    declarations: [ActivitiesComponent, LoginComponent, TimexactivityComponent],
    imports: [
      CommonModule,
      CoreRoutingModule,
      PerfectScrollbarModule,
      ReactiveFormsModule
    ],
    providers: [
        {
            provide: PERFECT_SCROLLBAR_CONFIG,
            useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
        }
      ]
  })
  export class CoreModule { }