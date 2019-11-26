import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './core/login/login.component';
import { ActivitiesComponent } from './core/activities/activities.component';
import { TimexactivityComponent } from './core/timexactivity/timexactivity/timexactivity.component';
import { StorageServiceModule } from 'ngx-webstorage-service';

import {
  HTTP_INTERCEPTORS
} from '@angular/common/http';
import {
  ApiAuthorization
} from './interceptors/api-authorization';

//import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoaderScreenInterceptor } from './interceptors/loaderscreen';
//import { GridModule } from '@progress/kendo-angular-grid';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ActivitiesComponent,
    TimexactivityComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    StorageServiceModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: ApiAuthorization,
    multi: true
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: LoaderScreenInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
