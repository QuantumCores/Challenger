import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodAddIngridientComponent } from './food-add-ingridient.component';

describe('FoodAddIngridientComponent', () => {
  let component: FoodAddIngridientComponent;
  let fixture: ComponentFixture<FoodAddIngridientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoodAddIngridientComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FoodAddIngridientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
