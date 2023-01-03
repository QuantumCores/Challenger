import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MealDishItemComponent } from './meal-dish-item.component';

describe('MealDishItemComponent', () => {
  let component: MealDishItemComponent;
  let fixture: ComponentFixture<MealDishItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MealDishItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MealDishItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
