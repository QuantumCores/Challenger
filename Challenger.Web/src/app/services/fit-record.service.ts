import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs'
import { FitRecordDto } from '../components/fit-record-item/fitRecordDto';
import { environment } from 'src/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class FitRecordService {

  //private apiUrl = 'https://localhost:7099/FitRecord'
  private apiUrl = `${environment.httpDomain}/FitRecord`;

  constructor(private http: HttpClient) { }

  public getFitRecords(): Observable<FitRecordDto[]> {
    return this.http.get<FitRecordDto[]>(this.apiUrl)
  }

  public deleteFitRecord(measurement: FitRecordDto): Observable<FitRecordDto> {
    const url = `${this.apiUrl}?id=${measurement.id}`;
    return this.http.delete<FitRecordDto>(url);
  }

  public updateFitRecord(measurement: FitRecordDto): Observable<FitRecordDto> {
    return this.http.patch<FitRecordDto>(this.apiUrl, measurement, httpOptions);
  }

  public addFitRecord(measurement: FitRecordDto): Observable<FitRecordDto> {
    return this.http.post<FitRecordDto>(this.apiUrl, measurement, httpOptions);
  }
}
