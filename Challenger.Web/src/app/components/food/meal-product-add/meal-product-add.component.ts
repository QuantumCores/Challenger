import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MealProductService } from 'src/app/services/meal-product.service';
import { ProductDto } from '../product-add/ProductDto';
import { MealProductDto } from './MealProductDto';

@Component({
  selector: 'app-meal-product-add',
  templateUrl: './meal-product-add.component.html',
  styleUrls: ['./meal-product-add.component.css']
})
export class MealProductAddComponent implements OnInit {

  isAddingNewProduct: boolean = false;
  isSearchingProduct: boolean = false;
  product: ProductDto;
  mealProduct: MealProductDto;
  errorMessage: string;

  @Input() mealRecordId: number;
  @Output() onMealProductAdded: EventEmitter<MealProductDto> = new EventEmitter<MealProductDto>();

  constructor(private mealProductService: MealProductService) { }

  ngOnInit(): void {
    this.mealProduct = new MealProductDto();
    this.mealProduct.size = 0;
  }

  findProduct(): void {
    this.isSearchingProduct = !this.isSearchingProduct;
    this.isAddingNewProduct = false;
  }

  addNewProduct(): void {
    this.isAddingNewProduct = !this.isAddingNewProduct;
    this.isSearchingProduct = false;
  }

  productAdded(product: ProductDto) {
    this.isSearchingProduct = false;
    this.isAddingNewProduct = false;

    this.mealProduct = new MealProductDto();
    this.product = product;
    this.mealProduct.productId = product.id;
  }

  validate() {
    return this.mealProduct.size && this.mealProduct.size != 0;
  }

  submit(): void {
    if (this.validate()) {
      this.calculateNutrition(this.mealProduct);
      this.mealProduct.mealRecordId = this.mealRecordId;
      this.mealProductService.addMealProduct(this.mealProduct).subscribe(
        {
          next: (mealProduct) => {
            this.onMealProductAdded.emit(mealProduct);
          },
          error: (error) => {
            this.errorMessage = error.status + ' - ' + error.statusText;
          }
        }
      );
    }
  }

  private calculateNutrition(mealProductToAdd: MealProductDto): void {
    const ratio = mealProductToAdd.size / this.product.size;
    mealProductToAdd.energy = Math.floor(this.product.energy * ratio);
    mealProductToAdd.carbohydrates = Math.floor(this.product.carbohydrates * ratio);
    mealProductToAdd.proteins = Math.floor(this.product.proteins * ratio);
    mealProductToAdd.fats = Math.floor(this.product.fats * ratio);    
    this.mealProduct.productName = this.product.name;
  }
}
