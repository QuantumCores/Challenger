import { TestBed } from '@angular/core/testing';

import { GymRecordService } from './gym-record.service';

describe('GymRecordService', () => {
  let service: GymRecordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GymRecordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
