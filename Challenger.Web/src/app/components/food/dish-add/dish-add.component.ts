import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DishService } from 'src/app/services/dish.service';
import { IngridientDto } from '../ingridient-add/IngridientDto';
import { DishDto } from './DishDto';

@Component({
  selector: 'app-dish-add',
  templateUrl: './dish-add.component.html',
  styleUrls: ['./dish-add.component.css']
})
export class DishAddComponent implements OnInit {

  isAddingIngridient: boolean = false;
  errorMessage: string;

  @Input() mealRecordId: number;
  @Input() dish: DishDto;
  @Output() onDishAdded: EventEmitter<DishDto> = new EventEmitter<DishDto>();

  constructor(private dishService: DishService) { }

  ngOnInit(): void {
    if (!this.dish) {
      this.dish = new DishDto();
      this.dish.size = 0;
      this.dish.servings = 1;
      this.dish.ingridients = [];
    }
  }

  addIngridient() {
    this.isAddingIngridient = !this.isAddingIngridient;
  }

  onIngridientAdded(ingridient: IngridientDto): void {
    this.dish.ingridients.push(ingridient);
    this.isAddingIngridient = false;
  }

  onDeleteIngridient(ingridient: IngridientDto): void{
    const index = this.dish.ingridients.indexOf(ingridient);
    if (index > -1) {
      this.dish.ingridients.splice(index, 1);
    }
  }

  addNewDish(): void {
    this.errorMessage = '';
    this.calculateNutrition(this.dish);
    this.dishService.addDish(this.dish).subscribe(
      {
        next: (dish) => {
          this.isAddingIngridient = false;
          this.onDishAdded.emit(dish);
        },
        error: (error) => { error.status + ' - ' + error.statusText; }
      }
    )
  }

  private calculateNutrition(dish: DishDto): void {
    dish.size = this.dish.ingridients.reduce((a, b) => a + b.size, 0);
    dish.energy = this.dish.ingridients.reduce((a, b) => a + b.energy, 0);
    dish.carbohydrates = this.dish.ingridients.reduce((a, b) => a + b.carbohydrates, 0);
    dish.proteins = this.dish.ingridients.reduce((a, b) => a + b.proteins, 0);
    dish.fats = this.dish.ingridients.reduce((a, b) => a + b.fats, 0);
  }
}
