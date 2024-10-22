import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth/Services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements AfterViewInit, OnInit {
  showLayout = false;
  constructor(private router: Router, private authService: AuthService) { }
  ngOnInit(): void {
    this.getAllSecretKeywords();
  }

  getAllSecretKeywords() {
    this.authService.GetAllSecretKeywords().subscribe((result) => {
      localStorage.setItem('secretKeywords', JSON.stringify(result));
    });
  }

  ngAfterViewInit(): void {
    const specificRoute = '/OurStory';
    if (this.router.url.includes(specificRoute)) {
      // The specific route is part of the current URL
      this.showLayout = true;
    } else {
      // The specific route is not part of the current URL
      this.showLayout = false;
    }
  }

}
