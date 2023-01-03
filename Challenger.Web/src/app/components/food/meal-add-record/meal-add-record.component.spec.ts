import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MealAddRecordComponent } from './meal-add-record.component';

describe('MealAddRecordComponent', () => {
  let component: MealAddRecordComponent;
  let fixture: ComponentFixture<MealAddRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MealAddRecordComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MealAddRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
