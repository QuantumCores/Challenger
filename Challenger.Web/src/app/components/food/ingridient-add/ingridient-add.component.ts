import { Component, Input, EventEmitter, OnInit, Output } from '@angular/core';
import { IngridientService } from 'src/app/services/ingridient.service';
import { ProductDto } from '../product-add/ProductDto';
import { IngridientDto } from './IngridientDto';

@Component({
  selector: 'app-ingridient-add',
  templateUrl: './ingridient-add.component.html',
  styleUrls: ['./ingridient-add.component.css']
})
export class IngridientAddComponent implements OnInit {

  isAddingNewProduct: boolean = false;
  isSearchingProduct: boolean = false;
  product: ProductDto;
  ingridient: IngridientDto;
  errorMessage: string;

  @Input() dishId: number;
  @Output() onIngridientAdded: EventEmitter<IngridientDto> = new EventEmitter<IngridientDto>();

  constructor(private ingridientService: IngridientService) { }

  ngOnInit(): void {
    this.ingridient = new IngridientDto();
    this.ingridient.size = 0;
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

    this.ingridient = new IngridientDto();
    this.product = product;
    this.ingridient.productId = product.id;
  }

  validate() {
    return this.ingridient.size && this.ingridient.size != 0;
  }

  submit(): void {
    if (this.validate()) {
      this.calculateNutrition(this.ingridient);
      this.ingridient.dishId = this.dishId;
      if (this.dishId && this.dishId != 0) {
        this.ingridientService.addIngridient(this.ingridient).subscribe(
          {
            next: (ingridient) => {
              this.onIngridientAdded.emit(ingridient);
            },
            error: (error) => {
              this.errorMessage = error.status + ' - ' + error.statusText;
            }
          }
        );
      }
      else{
        this.onIngridientAdded.emit(this.ingridient);
      }
    }
  }

  private calculateNutrition(ingridientToAdd: IngridientDto): void {
    const ratio = ingridientToAdd.size / this.product.size;
    ingridientToAdd.energy = Math.floor(this.product.energy * ratio);
    ingridientToAdd.carbohydrates = Math.floor(this.product.carbohydrates * ratio);
    ingridientToAdd.proteins = Math.floor(this.product.proteins * ratio);
    ingridientToAdd.fats = Math.floor(this.product.fats * ratio);
    ingridientToAdd.productName = this.product.name;
  }
}
