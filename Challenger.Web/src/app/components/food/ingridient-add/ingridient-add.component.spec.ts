import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IngridientAddComponent } from './ingridient-add.component';

describe('IngridientAddComponent', () => {
  let component: IngridientAddComponent;
  let fixture: ComponentFixture<IngridientAddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IngridientAddComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IngridientAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
