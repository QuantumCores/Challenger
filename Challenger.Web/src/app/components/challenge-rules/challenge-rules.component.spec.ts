import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChallengeRulesComponent } from './challenge-rules.component';

describe('ChallengeRulesComponent', () => {
  let component: ChallengeRulesComponent;
  let fixture: ComponentFixture<ChallengeRulesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChallengeRulesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChallengeRulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
