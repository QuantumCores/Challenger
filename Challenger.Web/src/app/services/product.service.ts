import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ProductDto } from '../components/food/product-add/ProductDto';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class ProductService {  
  
  private apiUrl = `${environment.httpDomain}/Product`;

  constructor(private http: HttpClient) { }

  public addProduct(product: ProductDto): Observable<ProductDto> {
    const url = `${this.apiUrl}`;
    return this.http.post<ProductDto>(url, product, httpOptions);
  }

  public findProduct(productSearchPhrase:string): Observable<ProductDto[]> {
    const url = this.apiUrl + '/Find?search=' + productSearchPhrase;
    return this.http.get<ProductDto[]>(url)
  }
}
