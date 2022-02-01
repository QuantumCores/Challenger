import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterModel } from '../components/register/RegisterModel';
import { Observable } from 'rxjs';
import { LoginModel } from '../components/login/LoginModel';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private apiUrl = 'https://localhost:7099/Account/api/v1'

  constructor(private http: HttpClient) { }

  public register(registerModel: RegisterModel): Observable<any> {
    const url = `${this.apiUrl}/register`;
    return this.http.post<RegisterModel>(url, registerModel, httpOptions);
  }

  public login(registerModel: LoginModel): Observable<any> {
    const url = `${this.apiUrl}/login`;
    return this.http.post<LoginModel>(url, registerModel, httpOptions);
  }
}
