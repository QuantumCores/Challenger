import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MealDishAddComponent } from './meal-dish-add.component';

describe('MealDishAddComponent', () => {
  let component: MealDishAddComponent;
  let fixture: ComponentFixture<MealDishAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MealDishAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MealDishAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
