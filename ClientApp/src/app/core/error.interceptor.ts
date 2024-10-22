import { Router, RouterModule } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Injectable, Injector } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private router: Router,
    private inject: Injector,
  ) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let toastr = this.inject.get(ToastrService)

        if (error.error.message == "jwt malformed" ||
          error.error.message == "invalid token") {
          toastr.error()
        } else if (error.error.message == "jwt must be provided" ||
          error.error.message == "Not Authenticated..") {
          toastr.error()
        } else if (error.error.message == "jwt expired") {
          toastr.error()
        } else { toastr.error() }

        // this.toastr.error(error.error.message)
        // toastr.error(error.error.message)
        // toastr.error(translate.instant("toaster.error"))




        if (error.error.message == "Not Authenticated.." ||
          error.error.message == "jwt expired" ||
          error.error.message == "jwt malformed" ||
          error.error.message == "invalid token" ||
          error.error.message == "jwt must be provided") {
          this.router.navigate(['/login'])
          localStorage.removeItem('token')
        }
        throw error
      })
    );
  }
}
