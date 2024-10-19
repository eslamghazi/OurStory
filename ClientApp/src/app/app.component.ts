import { AfterViewInit, Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements AfterViewInit {
  showLayout = false;
  constructor(private router: Router) {}

  ngAfterViewInit(): void {
    const specificRoute = '/OurStory';
    if (this.router.url.includes(specificRoute)) {
      // The specific route is part of the current URL
      this.showLayout = true;
    } else {
      // The specific route is not part of the current URL
      this.showLayout = false;
    }
    debugger;
  }
}
