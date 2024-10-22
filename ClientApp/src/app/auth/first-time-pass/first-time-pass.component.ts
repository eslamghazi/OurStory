import { AuthService } from './../Services/auth.service';
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
  secretKeywords = JSON.parse(localStorage.getItem('secretKeywords'))

  constructor(private router: Router) { }

  ngOnInit(): void {

  }


  signIn(password: string) {
    let firstTimePass = this.secretKeywords.find(x => x.title == "firstTimePass");
    console.log(firstTimePass);

    if (password == firstTimePass.keyword) {
      localStorage.setItem('firstTimePass', 'true');
      this.router.navigateByUrl('/auth/login');
    }
  }

}
