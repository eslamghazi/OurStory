import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FirstTimePassGuard, LoginGuard, OtherRoutesGuard } from './access-path.guard';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';
import { LoaderInterceptor } from './loader.interceptor';
import { ErrorInterceptor } from './error.interceptor';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    FirstTimePassGuard,
    LoginGuard,
    OtherRoutesGuard,
  ],
})
export class CoreModule { }
