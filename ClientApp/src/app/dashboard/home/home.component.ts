import { DashboardService } from './../Services/dashboard.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.getAllBlogs();
  }

  getAllBlogs() {
    this.dashboardService.getAllBlogs().subscribe((result) => {
      console.log(result);
    });
  }
}
