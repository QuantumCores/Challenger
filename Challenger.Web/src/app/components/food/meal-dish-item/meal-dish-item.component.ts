import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MealDishService } from 'src/app/services/meal-dish.service';
import { MealDishDto } from '../meal-dish-add/MealDishDto';


@Component({
  selector: 'app-meal-dish-item',
  templateUrl: './meal-dish-item.component.html',
  styleUrls: ['./meal-dish-item.component.css']
})
export class MealDishItemComponent implements OnInit {

  @Input() mealDish: MealDishDto;
  @Output() onDeleteMealDish: EventEmitter<MealDishDto> = new EventEmitter<MealDishDto>();

  errorMessage: string;

  constructor(private mealDishService: MealDishService) { }

  ngOnInit(): void {
  }

  onUpdate(): void {

  }

  onDelete(): void {
    this.errorMessage = '';
    this.mealDishService.deleteMealDish(this.mealDish.id).subscribe(
      {
        next: () => {          
          this.onDeleteMealDish.emit(this.mealDish);
        },
        error: (error: any) => {
          this.errorMessage = error.status + ' - ' + error.statusText;
        }
      });
  }

}
