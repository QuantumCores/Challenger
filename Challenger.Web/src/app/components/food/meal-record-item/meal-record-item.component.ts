import { Component, Input, OnInit } from '@angular/core';
import { MealRecordDto } from '../meal-add-record/MealRecordDto';
import { MealProductDto } from '../meal-product-add/MealProductDto';
import { ProductDto } from '../product-add/ProductDto';

@Component({
  selector: 'app-meal-record-item',
  templateUrl: './meal-record-item.component.html',
  styleUrls: ['./meal-record-item.component.css']
})
export class MealRecordItemComponent implements OnInit {

  isAddingFast: boolean = false;
  isAddingProduct: boolean = false;
  isAddingDish: boolean = false;

  @Input() record: MealRecordDto;
  constructor() { }

  ngOnInit(): void {
    // if (!this.record.products) {
    //   this.record.products = [];

    // }

    // if (!this.record.dishes) {
    //   this.record.dishes = [];
    // }
  }

  addFast() {
    this.isAddingFast = !this.isAddingFast;
    this.isAddingProduct = false;
    this.isAddingDish = false;
  }

  addProduct() {
    this.isAddingProduct = !this.isAddingProduct;
    this.isAddingFast = false;
    this.isAddingDish = false;
  }

  addDish() {
    this.isAddingDish != this.isAddingDish;
    this.isAddingDish = false;
    this.isAddingProduct = false;
  }

  mealProductAdded(product: MealProductDto) {
    this.record.mealProducts.push(product);
  }

  onDeleteMealProduct(mealProduct: MealProductDto) {
    let index = this.record.mealProducts.indexOf(mealProduct);
    if (index > -1) {
      this.record.mealProducts.splice(index, 1);
    }
  }
}
