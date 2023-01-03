import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FastRecordDto } from '../components/food/fast-record-add/FastRecordDto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class FastRecordService {

    
  private apiUrl = `${environment.httpDomain}/FastRecord`;

  constructor(private http: HttpClient) { }

  public addFastRecord(product: FastRecordDto): Observable<FastRecordDto> {
    const url = `${this.apiUrl}`;
    return this.http.post<FastRecordDto>(url, product, httpOptions);
  }

  deleteFastRecord(fastRecordId: number): Observable<any> {
    const url = `${this.apiUrl}?id=${fastRecordId}`;
    return this.http.delete<FastRecordDto>(url, httpOptions);
  }
}
