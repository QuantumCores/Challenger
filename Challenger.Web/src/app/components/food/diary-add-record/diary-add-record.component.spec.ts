import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DiaryAddRecordComponent } from './diary-add-record.component';

describe('DiaryAddRecordComponent', () => {
  let component: DiaryAddRecordComponent;
  let fixture: ComponentFixture<DiaryAddRecordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DiaryAddRecordComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DiaryAddRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
