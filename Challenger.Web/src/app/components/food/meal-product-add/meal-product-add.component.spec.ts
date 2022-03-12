import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MealProductAddComponent } from './meal-product-add.component';

describe('MealProductAddComponent', () => {
  let component: MealProductAddComponent;
  let fixture: ComponentFixture<MealProductAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MealProductAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MealProductAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
