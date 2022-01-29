import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GymRecordsComponent } from './gym-records.component';

describe('GymRecordsComponent', () => {
  let component: GymRecordsComponent;
  let fixture: ComponentFixture<GymRecordsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GymRecordsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GymRecordsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
