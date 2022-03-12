import { TestBed } from '@angular/core/testing';

import { DiaryRecordService } from './diary-record.service';

describe('DiaryRecordService', () => {
  let service: DiaryRecordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DiaryRecordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
