import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { RulesDto } from '../components/challenge-rules/RulesDto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class RulesService {

  private apiUrl = `${environment.httpDomain}/Rules`;

  constructor(private http: HttpClient) { }

  public getRules(): Observable<RulesDto> {
    return this.http.get<RulesDto>(this.apiUrl);
  }
}
