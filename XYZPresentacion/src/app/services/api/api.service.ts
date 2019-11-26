import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) {
    
  }

  public getStatus(): Observable<any> {
    return this.http.get(environment.url + 'api/Status', { responseType: 'text' })
    .pipe(catchError((this.handleError)));
  }

  public getApi(path: string, parameters: string = ''): Observable<any> {
    debugger;
    return this.http.get(environment.url + path + parameters)
      .pipe(map(response => response),
      catchError(this.handleError));
  }

  public postApi(path: string, data: any): Observable<any> {
    if (!data) {
      data = {};
    }
    return this.http.post(environment.url + path, data)
      .pipe(map(response => response),
      catchError(this.handleError));
  }

  public putApi(path: string, data: any): Observable<any> {
    if (!data) {
      data = {};
    }
    return this.http.put(environment.url + path, data)
      .pipe(map(response => response),
      catchError(this.handleError));
  }

  public deleteApi(path: string): Observable<any> {
    return this.http.delete(environment.url + path)
      .pipe(map(response => response),
      catchError(this.handleError));
  }
  private handleError(error: HttpErrorResponse) {
    // Error en el cliente
    debugger
    if (error.error instanceof ErrorEvent) {

      //Helper.swalAlerta('error', error.error.message);
      alert(error.error.message);
      // A client-side or network error occurred. Handle it accordingly.
      return Observable.throw({});
    } else {
      // Error en el frond
      let mensajeError = '';
      if (typeof error === 'object') {
        if (error.message) {
          mensajeError = error.message;
        }
      } else if (typeof error === 'string') {
        try {
          mensajeError = JSON.parse(error).message;
        } catch (e) {
          mensajeError = error;
        }
      }
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      if (error.status == 401) {
        //Helper.swalAlerta('error', "No esta autorizado para ingresar a este modulo");
        alert("No esta autorizado para ingresar a este modulo");
      }
      else {
        if (error.status == 302) {
          //Helper.swalAlerta('error', "Su Sesión ha caducado, debe ingresar nuevamente al sistema");
          alert("Su Sesión ha caducado, debe ingresar nuevamente al sistema");
        }
        else {
          if (error.status == 500) {
            //Helper.swalAlerta('error', "Error general en el sistema");
            alert("Error general en el sistema");
          }
          else {
            //Helper.swalAlerta('error', mensajeError);
            alert(mensajeError);
          }
        }
      }
      // Helper.swalAlerta('error', mensajeError);
      return Observable.throw({ 'status': error.status, 'message': error.error });
    }
    // return an observable with a user-facing error message
    // return Observaoble.thrw('Algo ocurrio intente de nuevo mas tarde.');
  }
}
