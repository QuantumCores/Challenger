import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MeasurementDto } from '../components/measurement-item/measurementDto';
import { Observable } from 'rxjs'
import { environment } from 'src/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class MeasurementService {

  private apiUrl = `${environment.httpDomain}/Measurement`;
  //private apiUrl = 'http://54.37.137.86:81/Measurement';

  constructor(private http: HttpClient) { }

  public getMeasurements(): Observable<MeasurementDto[]> {
    return this.http.get<MeasurementDto[]>(this.apiUrl)
  }

  public deleteMeasurement(measurement: MeasurementDto): Observable<MeasurementDto> {
    const url = `${this.apiUrl}?id=${measurement.id}`;
    return this.http.delete<MeasurementDto>(url, httpOptions);
  }

  public updateMeasurement(measurement: MeasurementDto): Observable<MeasurementDto> {
    return this.http.patch<MeasurementDto>(this.apiUrl, measurement, httpOptions);
  }

  public addMeasurement(measurement: MeasurementDto): Observable<MeasurementDto> {
    return this.http.post<MeasurementDto>(this.apiUrl, measurement, httpOptions);
  }
}
