import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  baseUrl = environment.backEndUrl
  constructor(private http: HttpClient) { }

  getAllBlogs() {
    return this.http.get<[]>(this.baseUrl + `Blogs/GetAllBlogs`)
  }
}
