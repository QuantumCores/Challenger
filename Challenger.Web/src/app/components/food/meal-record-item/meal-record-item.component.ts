import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FastRecordDto } from '../fast-record-add/FastRecordDto';
import { MealRecordDto } from '../meal-add-record/MealRecordDto';
import { MealDishDto } from '../meal-dish-add/MealDishDto';
import { MealProductDto } from '../meal-product-add/MealProductDto';

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
  @Output() onFoodAdded: EventEmitter<boolean> = new EventEmitter<boolean>();
  constructor() { }

  ngOnInit(): void {
    // if (!this.record.products) {
    //   this.record.products = [];

    // }

    // if (!this.record.dishes) {
    //   this.record.dishes = [];
    // }
  }

  getTime(): string {
    const minutes = this.record.recordTime % 60;
    return this.withZero((this.record.recordTime - minutes) / 60) + ':' + this.withZero(minutes);
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
    this.isAddingDish = !this.isAddingDish;
    this.isAddingFast = false;
    this.isAddingProduct = false;
  }

  mealProductAdded(product: MealProductDto) {
    this.record.mealProducts.push(product);
    this.resetAndEmitEvent()
  }

  fastRecordAdded(fastRecord: FastRecordDto) {
    this.record.fastRecords.push(fastRecord);
    this.resetAndEmitEvent()
  }

  mealDishAdded(mealDish: MealDishDto) {
    this.record.mealDishes.push(mealDish);
    this.resetAndEmitEvent()
  }

  onDeleteMealProduct(mealProduct: MealProductDto) {
    let index = this.record.mealProducts.indexOf(mealProduct);
    if (index > -1) {
      this.record.mealProducts.splice(index, 1);
      this.onFoodAdded.emit(true);
    }
  }

  onDeleteFastRecord(fastRecord: FastRecordDto) {
    let index = this.record.fastRecords.indexOf(fastRecord);
    if (index > -1) {
      this.record.fastRecords.splice(index, 1);
      this.onFoodAdded.emit(true);
    }
  }

  onDeleteMealDish(mealDish: MealDishDto) {
    let index = this.record.mealDishes.indexOf(mealDish);
    if (index > -1) {
      this.record.mealDishes.splice(index, 1);
      this.onFoodAdded.emit(true);
    }
  }

  private resetAndEmitEvent() {
    this.isAddingDish = false;
    this.isAddingFast = false;
    this.isAddingProduct = false;

    this.onFoodAdded.emit(true);
  }

  private withZero(n: number): string {
    if (n < 10 && n >= 0) {
      return '0' + n;
    }

    return '' + n;
  }
}
