import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FirstTimePassGuard, LoginGuard, OtherRoutesGuard } from './access-path.guard';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    FirstTimePassGuard,
    LoginGuard,
    OtherRoutesGuard,
  ],
})
export class CoreModule { }
