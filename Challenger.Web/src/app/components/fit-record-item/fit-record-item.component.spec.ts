import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FitRecordItemComponent } from './fit-record-item.component';

describe('FitRecordItemComponent', () => {
  let component: FitRecordItemComponent;
  let fixture: ComponentFixture<FitRecordItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FitRecordItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FitRecordItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
