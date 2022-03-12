import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProductService } from 'src/app/services/product.service';
import { ProductDto } from './ProductDto';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.css']
})
export class ProductAddComponent implements OnInit {

  addProductForm: FormGroup;
  errorMessage: string;

  @Output() onProductAdded: EventEmitter<ProductDto> = new EventEmitter<ProductDto>();

  constructor(private foodService: ProductService) { }

  ngOnInit(): void {
    this.addProductForm = new FormGroup({
      name: new FormControl("", [Validators.required, Validators.minLength(3), Validators.maxLength(64)]),
      brand: new FormControl("", [Validators.minLength(3), Validators.maxLength(32)]),
      barcode: new FormControl("", [Validators.minLength(13), Validators.maxLength(13)]),
      size: new FormControl("", [Validators.required]),
      sizeUnit: new FormControl("", [Validators.required]),
      energy: new FormControl("", [Validators.required]),
      fats: new FormControl("", [Validators.required]),
      proteins: new FormControl("", [Validators.required]),
      carbohydrates: new FormControl("", [Validators.required]),
    });
  }

  public validateControl = (controlName: string) => {
    return this.addProductForm.controls[controlName].invalid && this.addProductForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.addProductForm.controls[controlName].hasError(errorName)
  }

  submit(productToAdd: any): void {
    this.errorMessage = '';
    this.foodService.addProduct(productToAdd).subscribe(
      {
        next: (product) => {
          this.onProductAdded.emit(product);
        },
        error: (error) => {
          this.errorMessage = error.status + ' - ' + error.statusText;
        }
      })
  }
}
