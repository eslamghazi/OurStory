import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { FirstTimePassGuard, LoginGuard } from '../core/access-path.guard';
import { FirstTimePassComponent } from './first-time-pass/first-time-pass.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, canActivate: [LoginGuard] },
  { path: 'firstTimePass', component: FirstTimePassComponent, canActivate: [FirstTimePassGuard] },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule { }
