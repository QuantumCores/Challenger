import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MealRecordDto } from '../components/food/meal-add-record/MealRecordDto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class MealRecordService {

  private apiUrl = `${environment.httpDomain}/MealRecord`;

  constructor(private http: HttpClient) { }

  public getMealRecords(): Observable<MealRecordDto[]> {
    return this.http.get<MealRecordDto[]>(this.apiUrl)
  }

  public addMealRecord(record: MealRecordDto): Observable<MealRecordDto> {    
    return this.http.post<MealRecordDto>(this.apiUrl, record, httpOptions);
  }
}
