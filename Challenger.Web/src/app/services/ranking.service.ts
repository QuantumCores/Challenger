import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserScoresDto } from '../components/ranking/UserScoresDto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class RankingService {

  private apiUrl = `${environment.httpDomain}/Ranking`;

  constructor(private http: HttpClient) { }

  public getUsersScores(): Observable<UserScoresDto[]> {
    return this.http.get<UserScoresDto[]>(this.apiUrl);
  }


}
