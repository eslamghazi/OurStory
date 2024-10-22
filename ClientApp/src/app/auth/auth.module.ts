import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { FirstTimePassComponent } from './first-time-pass/first-time-pass.component';


@NgModule({
  declarations: [
    FirstTimePassComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule
  ]
})
export class AuthModule { }
