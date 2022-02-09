import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserBasicDto } from '../components/measurements/UserBasicDto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = `${environment.httpDomain}/User`;

  constructor(private http: HttpClient) { }

  public getUserBasic(): Observable<UserBasicDto> {
    const url = `${this.apiUrl}/api/v1/basic`;
    return this.http.get<UserBasicDto>(url);
  }
}
