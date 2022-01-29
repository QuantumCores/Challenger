import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GymRecordItemComponent } from './gym-record-item.component';

describe('GymRecordItemComponent', () => {
  let component: GymRecordItemComponent;
  let fixture: ComponentFixture<GymRecordItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GymRecordItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GymRecordItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
