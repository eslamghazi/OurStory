import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-first-time-pass',
  templateUrl: './first-time-pass.component.html',
  styleUrls: ['./first-time-pass.component.scss']
})
export class FirstTimePassComponent implements OnInit {
  currentYear = new Date().getFullYear();
  previousYear = new Date().getFullYear() - 1;

  constructor(private router: Router) { }

  ngOnInit(): void { }

  signIn(password: string) {
    console.log(password);

    if (password == 'MabascotaStolenMaHeart') {
      localStorage.setItem('firstTimePass', 'true');
      this.router.navigateByUrl('/auth/login');
    }
  }

}
