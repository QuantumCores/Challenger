import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { RegisterModel } from '../components/register/RegisterModel';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MealProductDto } from '../components/food/meal-product-add/MealProductDto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class MealProductService {

  private apiUrl = `${environment.httpDomain}/MealProduct`;

  constructor(private http: HttpClient) { }

  public addMealProduct(mealProduct: MealProductDto): Observable<MealProductDto> {
    const url = `${this.apiUrl}`;
    return this.http.post<MealProductDto>(url, mealProduct, httpOptions);
  }

  public deleteMealProduct(mealProductId: number): Observable<any> {
    const url = `${this.apiUrl}?id=${mealProductId}`;
    return this.http.delete<MealProductDto>(url, httpOptions);
  }
}
