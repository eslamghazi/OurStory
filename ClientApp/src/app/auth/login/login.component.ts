import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  currentYear = new Date().getFullYear();
  previousYear = new Date().getFullYear() - 1;

  constructor() { }

  ngOnInit(): void {
  }

  signIn(username: string, password: string) {
  }
}
