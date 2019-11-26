import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap, map, catchError } from 'rxjs/operators';
import { ApiService } from './../api/api.service';
import { environment, STORAGE_KEY } from './../../../environments/environment';
import { StorageService, SESSION_STORAGE } from 'ngx-webstorage-service';
import { Activities } from './../../interfaces/activities/activities';

@Injectable({
  providedIn: 'root'
})
export class ActivitiesService {
  constructor(private http: HttpClient, private apiService: ApiService,	@Inject(SESSION_STORAGE) private storage: StorageService)
  { }

  public GetActivities(): Observable<any> {
    //return this.apiService.postApi('Activities/GetActivities', activities)
    return this.apiService.getApi('Activities/GetActivities')
    .pipe(tap((data) => {
      debugger;
      console.log("data ", data.Data);
      // if(data.StatusCode == 200){
        
      // }
      // else{
      //   alert(data.Message);
      // }
    })
    // ,
    // catchError(this.handleError<any>('Cajamedimas'))
    );
  }

  public RegisterActivity(activity: Activities): Observable<any> {
    return this.apiService.postApi('Activities/RegisterActivity', activity)
    .pipe(
      tap((data) => {
        console.log(data, "Data")
      })
    );
  }

  // private handleError<T> (operation = 'operation', result?: T) {
  //   return (error: any): Observable<T> => {
  //     console.error(error);
  //     return of(result as T);
  //   };
  // }
}
