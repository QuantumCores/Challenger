import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FitRecordsComponent } from './fit-records.component';

describe('FitRecordsComponent', () => {
  let component: FitRecordsComponent;
  let fixture: ComponentFixture<FitRecordsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FitRecordsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FitRecordsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
