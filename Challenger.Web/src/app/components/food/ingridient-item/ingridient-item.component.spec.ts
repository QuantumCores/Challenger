import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IngridientItemComponent } from './ingridient-item.component';

describe('IngridientItemComponent', () => {
  let component: IngridientItemComponent;
  let fixture: ComponentFixture<IngridientItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IngridientItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IngridientItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
