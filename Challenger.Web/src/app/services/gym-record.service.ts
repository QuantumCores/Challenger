import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs'
import { GymRecordDto } from '../components/gym-record-item/gymRecordDto';
import { environment } from 'src/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class GymRecordService {

  private apiUrl = `${environment.httpDomain}/GymRecord`;

  constructor(private http: HttpClient) { }

  public getGymRecords(): Observable<GymRecordDto[]> {
    return this.http.get<GymRecordDto[]>(this.apiUrl)
  }

  public deleteGymRecord(measurement: GymRecordDto): Observable<GymRecordDto> {
    const url = `${this.apiUrl}?id=${measurement.id}`;
    return this.http.delete<GymRecordDto>(url);
  }

  public updateGymRecord(measurement: GymRecordDto): Observable<GymRecordDto> {
    return this.http.patch<GymRecordDto>(this.apiUrl, measurement, httpOptions);
  }

  public addGymRecord(measurement: GymRecordDto): Observable<GymRecordDto> {
    return this.http.post<GymRecordDto>(this.apiUrl, measurement, httpOptions);
  }
}
