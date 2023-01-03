import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IngridientDto } from '../components/food/ingridient-add/IngridientDto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class IngridientService {

  private apiUrl = `${environment.httpDomain}/Ingridient`;

  constructor(private http: HttpClient) { }

  public addIngridient(ingridient: IngridientDto): Observable<IngridientDto> {
    const url = `${this.apiUrl}`;
    return this.http.post<IngridientDto>(url, ingridient, httpOptions);
  }

  public deleteIngridient(ingridientId: number): Observable<any> {
    const url = `${this.apiUrl}?id=${ingridientId}`;
    return this.http.delete<IngridientDto>(url, httpOptions);
  }
}

