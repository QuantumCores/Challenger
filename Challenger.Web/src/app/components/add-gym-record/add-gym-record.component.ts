import { formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GymRecordDto } from '../gym-record-item/gymRecordDto';

@Component({
  selector: 'app-add-gym-record',
  templateUrl: './add-gym-record.component.html',
  styleUrls: ['./add-gym-record.component.css']
})
export class AddGymRecordComponent implements OnInit {

  @Input() record: GymRecordDto;
  @Output() onChangeGymRecord: EventEmitter<GymRecordDto> = new EventEmitter<GymRecordDto>();
  original: any;

  constructor() {
  }

  ngOnInit(): void {
    if (this.record) {
      this.original = { ...this.record };
    }
  }

  onChange(): void {
    if (this.hasChanged()) {
      this.onChangeGymRecord.emit(this.record);
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
}
