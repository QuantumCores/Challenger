import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodAddDishComponent } from './food-add-dish.component';

describe('FoodAddDishComponent', () => {
  let component: FoodAddDishComponent;
  let fixture: ComponentFixture<FoodAddDishComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoodAddDishComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FoodAddDishComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
