import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap, map, catchError } from 'rxjs/operators';
import { ApiService } from './../api/api.service';
import { environment, STORAGE_KEY } from './../../../environments/environment';
import { StorageService, SESSION_STORAGE } from 'ngx-webstorage-service';
import { Timexactivity } from './../../interfaces/timexactivity/timexactivity';

@Injectable({
  providedIn: 'root'
})
export class TimexactivityService {

  constructor(private http: HttpClient, private apiService: ApiService,	@Inject(SESSION_STORAGE) private storage: StorageService)
  { }

  public GetTimeXActivity(idActivity: number): Observable<any> {
    return this.apiService.getApi('TimeXActivity/GetTimeXActivity?idActivity='+ idActivity)
    .pipe(tap((data) => {
      debugger;
      console.log("data ", data.Data);
    })
    );
  }

  public RegisterTimeXActivity(timexactivity: Timexactivity): Observable<any> {
    return this.apiService.postApi('TimeXActivity/RegisterTimeXActivity', timexactivity)
    .pipe(
      tap((data) => {
        console.log(data, "Data")
      })
    );
  }
}
