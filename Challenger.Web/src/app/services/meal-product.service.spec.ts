import { TestBed } from '@angular/core/testing';

import { MealProductService } from './meal-product.service';

describe('MealProductService', () => {
  let service: MealProductService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MealProductService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
