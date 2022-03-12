import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MealProductItemComponent } from './meal-product-item.component';

describe('MealProductItemComponent', () => {
  let component: MealProductItemComponent;
  let fixture: ComponentFixture<MealProductItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MealProductItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MealProductItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
