import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MealProductService } from 'src/app/services/meal-product.service';
import { MealProductDto } from '../meal-product-add/MealProductDto';

@Component({
  selector: 'app-meal-product-item',
  templateUrl: './meal-product-item.component.html',
  styleUrls: ['./meal-product-item.component.css']
})
export class MealProductItemComponent implements OnInit {

  @Input() mealProduct: MealProductDto;
  @Output() onDeleteMealProduct: EventEmitter<MealProductDto> = new EventEmitter<MealProductDto>();

  errorMessage: string;

  constructor(private mealProductService: MealProductService) { }

  ngOnInit(): void {
  }

  onUpdate(): void {

  }

  onDelete(): void {
    this.errorMessage = '';
    this.mealProductService.deleteMealProduct(this.mealProduct.id).subscribe(
      {
        next: () => {
          this.onDeleteMealProduct.emit(this.mealProduct);
        },
        error: (error: any) => {
          this.errorMessage = error.status + ' - ' + error.statusText;
        }
      });
  }
}
