import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddGymRecordComponent } from './gym-add-record.component';

describe('AddGymRecordComponent', () => {
  let component: AddGymRecordComponent;
  let fixture: ComponentFixture<AddGymRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddGymRecordComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddGymRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
