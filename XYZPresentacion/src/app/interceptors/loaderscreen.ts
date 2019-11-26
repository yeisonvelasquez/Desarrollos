import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";
import { finalize } from "rxjs/operators";
import { LoaderService } from '../services/loader/loader.service';


@Injectable()
export class LoaderScreenInterceptor implements HttpInterceptor {

    activeRequests: number = 0;
 
    /**
     * URLs for which the loading screen should not be enabled
     */
    skippUrls = [
        '/authrefresh',
    ];

    constructor(private loaderService: LoaderService) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let displayLoadingScreen = true;

        for (const skippUrl of this.skippUrls) {
            if (new RegExp(skippUrl).test(request.url)) {
                displayLoadingScreen = false;
                break;
            }
        }

        if (displayLoadingScreen) {
            if (this.activeRequests === 0) {
                this.loaderService.show();
            }

            this.activeRequests++;
            return next.handle(request).pipe(
                finalize(() => {
                    this.activeRequests--;
                    if (this.activeRequests === 0) {
                        this.loaderService.hide();
                    }
                })
            )
        } else {
            return next.handle(request);
        }

    };

}