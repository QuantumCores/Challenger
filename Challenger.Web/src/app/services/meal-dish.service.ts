import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MealDishDto } from '../components/food/meal-dish-add/MealDishDto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class MealDishService {

  private apiUrl = `${environment.httpDomain}/MealDish`;

  constructor(private http: HttpClient) { }

  public addMealDish(mealProduct: MealDishDto): Observable<MealDishDto> {
    const url = `${this.apiUrl}`;
    return this.http.post<MealDishDto>(url, mealProduct, httpOptions);
  }

  public deleteMealDish(mealDishId: number): Observable<any> {
    const url = `${this.apiUrl}?id=${mealDishId}`;
    return this.http.delete<MealDishDto>(url, httpOptions);
  }
}
