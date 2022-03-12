import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DiaryRecordDto } from '../components/food/diary-add-record/DiaryRecordDto';
import { formatDate } from '@angular/common';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class DiaryRecordService {

  private apiUrl = `${environment.httpDomain}/DiaryRecord`;

  constructor(private http: HttpClient) { }

  public getDiaryRecords(startDate: Date): Observable<DiaryRecordDto[]> {
    const url = this.apiUrl + '?startDate=' + formatDate(startDate, 'yyyy-MM-dd', 'en_US');
    return this.http.get<DiaryRecordDto[]>(url)
  }

  public addDiaryRecord(record: DiaryRecordDto): Observable<DiaryRecordDto> {
    return this.http.post<DiaryRecordDto>(this.apiUrl, record, httpOptions);
  }
}
