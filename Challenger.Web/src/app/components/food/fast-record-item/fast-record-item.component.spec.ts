import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FastRecordItemComponent } from './fast-record-item.component';

describe('FastRecordItemComponent', () => {
  let component: FastRecordItemComponent;
  let fixture: ComponentFixture<FastRecordItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FastRecordItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FastRecordItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
