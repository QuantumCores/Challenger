import { formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FitRecordDto } from '../fit-record-item/fitRecordDto';
import { ActivityEnum } from './ActivityEnum';

@Component({
  selector: 'app-add-fit-record',
  templateUrl: './add-fit-record.component.html',
  styleUrls: ['./add-fit-record.component.css']
})
export class AddFitRecordComponent implements OnInit {

  @Input() record: FitRecordDto;
  @Output() onChangeFitRecord: EventEmitter<FitRecordDto> = new EventEmitter<FitRecordDto>();
  original: any;
  activities: string[];

  constructor() {
  }

  ngOnInit(): void {
    if (this.record) {
      this.original = { ...this.record };
    }

    this.activities = Object.keys(ActivityEnum).filter(x => !this.stringIsNumber(x));
  }

  onChange(): void {
    if (this.hasChanged()) {
      this.onChangeFitRecord.emit(this.record);
    }
  }

  getDate(): string {
    return formatDate(this.record.recordDate, 'yyyy-MM-dd', 'en-us');
  }

  setDate(event: any): void {
    this.record.recordDate = new Date(event.target.value);
  }

  hasChanged(): boolean {
    if (this.original) {
      let keys = Object.keys(this.original);
      let asAny = this.record as any;
      for (let i = 0; i < keys.length; i++) {
        let prop = keys[i];
        if (this.original[prop] !== asAny[prop]) {
          return true;
        }
      }
    }
    return false;
  }

  stringIsNumber(value: string): boolean {
    return isNaN(Number(value)) === false;
  }
}
