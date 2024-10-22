import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { OtherRoutesGuard } from '../core/access-path.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    canActivate: [OtherRoutesGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule { }
