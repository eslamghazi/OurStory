import { Component, OnInit } from '@angular/core';
import { AuthService } from '../Services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  currentYear = new Date().getFullYear();
  previousYear = new Date().getFullYear() - 1;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  signIn(username: string, password: string) {
    let model = {
      username: username,
      password: password
    }

    this.authService.Login(model).subscribe((result) => {
      if (!result || !result.token) return;
      localStorage.setItem("token", result.token)
      this.router.navigateByUrl('/ourStory');
    })
  }
}
