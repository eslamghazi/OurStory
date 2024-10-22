import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanDeactivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { SwalService } from '../shared/services/swal.service';

@Injectable({
  providedIn: 'root'
})
export class FirstTimePassGuard implements CanActivate {
  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private swal: SwalService
  ) { }
  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean | UrlTree> {
    this.spinner.show();
    let firstTimePass =
      localStorage.getItem('firstTimePass');
    let userInfo =
      localStorage.getItem('userInfo');
    if (!firstTimePass && !userInfo) {
      this.spinner.hide();
      return true;

    } else {
      this.router.navigate(['/auth/login']);
      this.swal.toastr('warning', 'انت مسجل بالفعل في هذه الصفحة');
      this.spinner.hide();
      return false;

    }
  }
}

@Injectable({
  providedIn: 'root',
})
export class LoginGuard implements CanActivate {
  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private swal: SwalService
  ) { }
  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean | UrlTree> {
    this.spinner.show();
    let firstTimePass =
      localStorage.getItem('firstTimePass');
    let userInfo = localStorage.getItem('userInfo');
    if (firstTimePass && !userInfo) {
      this.spinner.hide();
      return true;
    } else if (userInfo) {
      this.router.navigate(['/ourstory']);
      this.swal.toastr('warning', 'انت مسجل بالفعل في هذه الصفحة');
      this.spinner.hide();
      return false;
    }
    else {
      this.router.navigate(['/auth/firstTimePass']);
      this.swal.toastr('error', 'عفوًا ليس لديك صلاحية الدخول لهذه الصفحة');
      this.spinner.hide();
      return false;
    }
  }
}


@Injectable({
  providedIn: 'root'
})
export class OtherRoutesGuard implements CanActivate {
  constructor(
    private router: Router,
    private spinner: NgxSpinnerService,
    private swal: SwalService
  ) { }
  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean | UrlTree> {
    this.spinner.show();
    let userInfo =
      localStorage.getItem('userInfo');
    if (userInfo) {
      this.spinner.hide();
      return true;

    } else {
      this.router.navigate(['/auth/login']);
      this.swal.toastr('error', 'عفوًا ليس لديك صلاحية الدخول لهذه الصفحة');
      this.spinner.hide();
      return false;

    }
  }
}
