import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DishDto } from '../components/food/dish-add/DishDto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class DishService {

  private apiUrl = `${environment.httpDomain}/Dish`;

  constructor(private http: HttpClient) { }

  public addDish(dish: DishDto): Observable<DishDto> {
    const url = `${this.apiUrl}`;
    return this.http.post<DishDto>(url, dish, httpOptions);
  }

  public findDish(dishSearchPhrase:string): Observable<DishDto[]> {
    const url = this.apiUrl + '/Find?search=' + dishSearchPhrase;
    return this.http.get<DishDto[]>(url)
  }
}
