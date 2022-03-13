import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FastRecordAddComponent } from './fast-record-add.component';

describe('FastRecordAddComponent', () => {
  let component: FastRecordAddComponent;
  let fixture: ComponentFixture<FastRecordAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FastRecordAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FastRecordAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
