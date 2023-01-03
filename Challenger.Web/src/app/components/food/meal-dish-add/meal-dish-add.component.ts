import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MealDishService } from 'src/app/services/meal-dish.service';
import { DishDto } from '../dish-add/DishDto';
import { MealDishDto } from './MealDishDto';

@Component({
  selector: 'app-meal-dish-add',
  templateUrl: './meal-dish-add.component.html',
  styleUrls: ['./meal-dish-add.component.css']
})
export class MealDishAddComponent implements OnInit {

  isSearchingDish: boolean;
  isAddingNewDish: boolean;
  dish: DishDto;
  errorMessage: string;

  @Input() mealRecordId: number;
  @Input() mealDish: MealDishDto;
  @Output() onMealDishAdded: EventEmitter<MealDishDto> = new EventEmitter<MealDishDto>();

  constructor(private mealDishService: MealDishService) { }

  ngOnInit(): void {
    this.mealDish = new MealDishDto();
    this.mealDish.mealRecordId = this.mealRecordId;
    this.mealDish.servings = 0;
  }

  findDish(): void {
    this.isSearchingDish = !this.isSearchingDish;
    this.isAddingNewDish = false;
  }

  addNewDish(): void {
    this.isAddingNewDish = !this.isAddingNewDish;
    this.isSearchingDish = false;
  }

  dishAdded(dish: DishDto) {
    this.isSearchingDish = false;
    this.isAddingNewDish = false;

    this.mealDish = new MealDishDto();
    this.dish = dish;
    this.mealDish.dishId = dish.id;
  }

  validate() {
    return this.mealDish.servings && this.mealDish.servings != 0;
  }

  submit(): void {
    this.errorMessage = '';
    if (this.validate()) {
      this.calculateNutrition(this.mealDish);
      this.mealDish.mealRecordId = this.mealRecordId;
      this.mealDishService.addMealDish(this.mealDish).subscribe(
        {
          next: (mealDish) => {
            this.mealDish = null as unknown as MealDishDto;
            this.dish = null as unknown as DishDto;
            this.isAddingNewDish = false;
            this.isSearchingDish = false;
            
            this.onMealDishAdded.emit(mealDish);
          },
          error: (error) => {
            this.errorMessage = error.status + ' - ' + error.statusText;
          }
        }
      );
    }
  }

  private calculateNutrition(mealDishToAdd: MealDishDto): void {
    const ratio = mealDishToAdd.servings / this.dish.servings;
    mealDishToAdd.energy = Math.floor(this.dish.energy * ratio);
    mealDishToAdd.carbohydrates = Math.floor(this.dish.carbohydrates * ratio);
    mealDishToAdd.proteins = Math.floor(this.dish.proteins * ratio);
    mealDishToAdd.fats = Math.floor(this.dish.fats * ratio);
    this.mealDish.dishName = this.dish.name;
  }
}
