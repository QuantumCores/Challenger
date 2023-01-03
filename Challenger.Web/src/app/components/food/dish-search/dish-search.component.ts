import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DishService } from 'src/app/services/dish.service';
import { DishDto } from '../dish-add/DishDto';

@Component({
  selector: 'app-dish-search',
  templateUrl: './dish-search.component.html',
  styleUrls: ['./dish-search.component.css']
})
export class DishSearchComponent implements OnInit {
  
  dishSearchPhrase: string;
  results: DishDto[];
  errorMessage: string;

  @Output() onDishFound: EventEmitter<DishDto> = new EventEmitter<DishDto>()
  
  constructor(private dishService: DishService) { }

  ngOnInit(): void {
    this.results = [];
  }

  onDishSelected(Dish: DishDto): void {
    this.onDishFound.emit(Dish);
  }

  search(): void {
    this.errorMessage = '';
    this.dishService.findDish(this.dishSearchPhrase).subscribe(
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
