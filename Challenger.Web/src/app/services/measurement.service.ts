import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MeasurementDto } from '../components/measurement-item/measurementDto';
import { Observable } from 'rxjs'

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class MeasurementService {

  private apiUrl = 'https://localhost:7099/Measurement'

  constructor(private http: HttpClient) { }

  public getMeasurements(): Observable<MeasurementDto[]> {
    return this.http.get<MeasurementDto[]>(this.apiUrl)
  }

  public deleteMeasurement(measurement: MeasurementDto): Observable<MeasurementDto> {
    const url = `${this.apiUrl}/${measurement.id}`;
    return this.http.delete<MeasurementDto>(url);
  }

  public updateMeasurementReminder(measurement: MeasurementDto): Observable<MeasurementDto> {
    const url = `${this.apiUrl}/${measurement.id}`;
    return this.http.put<MeasurementDto>(url, MeasurementDto, httpOptions);
  }

  public addMeasurement(measurement: MeasurementDto): Observable<MeasurementDto> {
    return this.http.post<MeasurementDto>(this.apiUrl, measurement, httpOptions);
  }
}
