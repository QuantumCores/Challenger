import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MealRecordItemComponent } from './meal-record-item.component';

describe('MealRecordItemComponent', () => {
  let component: MealRecordItemComponent;
  let fixture: ComponentFixture<MealRecordItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MealRecordItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MealRecordItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
