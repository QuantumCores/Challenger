import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFitRecordComponent } from './fit-add-record.component';

describe('AddFitRecordComponent', () => {
  let component: AddFitRecordComponent;
  let fixture: ComponentFixture<AddFitRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddFitRecordComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddFitRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
