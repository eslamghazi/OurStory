import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.backEndUrl
  constructor(private http: HttpClient) { }

  GetAllSecretKeywords() {
    return this.http.get<[]>(this.baseUrl + `SecretKeywords/GetAllSecretKeywords`)
  }

  Login(model) {
    return this.http.post<any>(this.baseUrl + `Lovers/login`, model)
  }
}
