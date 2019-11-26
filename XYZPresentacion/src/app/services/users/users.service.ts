import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap, map, catchError } from 'rxjs/operators';
import { ApiService } from './../api/api.service';
import { environment, STORAGE_KEY } from './../../../environments/environment';
import { StorageService, SESSION_STORAGE } from 'ngx-webstorage-service';
import { User } from './../../interfaces/users/user';
@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient, private apiService: ApiService,	@Inject(SESSION_STORAGE) private storage: StorageService)
  { }

  // public loginUser(user: User): Observable<any> {
  //   const path = environment.url + 'User/Login';
  //   return this.http.post(path, user);
  //   //this.UsersService.loginUser().subscribe(result => { console.log(result);});
  // }

  public loginUser(user: User): Observable<any> {
    return this.apiService.postApi('User/Login', user)
    .pipe(tap((data) => {
      console.log("data token", data.Data);
      if(data.StatusCode == 200){
        this.createSession(data.Data);
      }
      else{
        alert(data.Message);
      }
    })
    );
  }

  public getUsers(){
    const path = environment.url + 'User/GetUsers';
    return this.http.get<User[]>(path);
    //this.UsersService.loginUser().subscribe(result => { console.log(result);});
  }

  validateToken() : Observable<any>{
		
		return this.apiService.getApi('api/Login/ValidateUser')
			.pipe(
			tap((data) => console.log(data)));
		}

	public createSession(data: any): void {
		this.storage.set(
			STORAGE_KEY, {
				token: data
			}
		);
	}

	public closeSession(): void {
		this.storage.clear();
	}
}
