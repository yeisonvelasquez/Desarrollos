import {
    Injectable,
    Inject
  } from '@angular/core';
  import {
    HttpInterceptor,
    HttpHandler,
    HttpRequest,
    HttpEvent,
    HttpResponse,
    HttpEventType,
    HttpHeaders
  } from '@angular/common/http';
  import {
    Observable,
    of
  } from 'rxjs';
  import {
    SESSION_STORAGE,
    StorageService
  } from 'ngx-webstorage-service';
  import {
    STORAGE_KEY
  } from '../../environments/environment';
  
  @Injectable()
  export class ApiAuthorization implements HttpInterceptor {
    public request_: HttpRequest<any>;
    constructor(
      @Inject(SESSION_STORAGE) private storage: StorageService,
    ) { }
  
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  
      debugger
      if (this.storage.has(STORAGE_KEY)) {
        const token = this.storage.get(STORAGE_KEY)['token'];
        if (token) {
          const headers = new HttpHeaders({
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json'
          });
          request = request.clone({ headers });
        }
      }
      return next.handle(request);
  
    }
  
  }