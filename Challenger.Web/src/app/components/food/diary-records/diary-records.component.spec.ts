import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiaryRecordsComponent } from './diary-records.component';

describe('DiaryRecordsComponent', () => {
  let component: DiaryRecordsComponent;
  let fixture: ComponentFixture<DiaryRecordsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DiaryRecordsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DiaryRecordsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
