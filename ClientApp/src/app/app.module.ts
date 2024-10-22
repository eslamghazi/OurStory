import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SharedModule } from './shared/shared.module';
import { LoginComponent } from './auth/login/login.component';
import { LayoutModule } from './dashboard/layout/layout.module';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [AppComponent, LoginComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    LayoutModule,
    CoreModule,
    HttpClientModule,
    NgxSpinnerModule.forRoot({ type: 'timer' }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
