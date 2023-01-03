import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { ProductDto } from '../product-add/ProductDto';

@Component({
  selector: 'app-product-search',
  templateUrl: './product-search.component.html',
  styleUrls: ['./product-search.component.css']
})
export class ProductSearchComponent implements OnInit {

  @Output() onProductFound: EventEmitter<ProductDto> = new EventEmitter<ProductDto>()
  productSearchPhrase: string;
  results: ProductDto[];
  errorMessage: string;

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.results = [];
  }

  onProductSelected(product: ProductDto): void {
    this.onProductFound.emit(product);
  }

  search(): void {
    this.errorMessage = '';
    this.productService.findProduct(this.productSearchPhrase).subscribe(
      {
        next: (result) => {
          this.results = result;
        },
        error: (error) => {
          this.errorMessage = error.status + ' - ' + error.statusText;
        }
      }
    );
  }
}
