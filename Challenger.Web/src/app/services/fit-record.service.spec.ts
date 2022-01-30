import { TestBed } from '@angular/core/testing';

import { FitRecordService } from './fit-record.service';

describe('FitRecordService', () => {
  let service: FitRecordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FitRecordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
