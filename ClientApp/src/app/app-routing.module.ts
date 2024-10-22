import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OtherRoutesGuard } from './core/access-path.guard';
import { HomeComponent } from './dashboard/home/home.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'OurStory',
    pathMatch: 'full',
  },

  {
    path: 'ourStory',
    loadChildren: () =>
      import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
    canActivate: [OtherRoutesGuard],
  },

  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then((m) => m.AuthModule),
  },

  { path: '**', pathMatch: 'full', component: HomeComponent, canActivate: [OtherRoutesGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
