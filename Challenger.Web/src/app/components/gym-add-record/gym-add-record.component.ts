import { formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GymRecordDto } from '../gym-record-item/gymRecordDto';
import { MuscleGroupsEnum } from './MuscleGroupsEnum';

@Component({
  selector: 'app-gym-add-record',
  templateUrl: './gym-add-record.component.html',
  styleUrls: ['./gym-add-record.component.css']
})
export class AddGymRecordComponent implements OnInit {

  @Input() record: GymRecordDto;
  @Output() onChangeGymRecord: EventEmitter<GymRecordDto> = new EventEmitter<GymRecordDto>();
  original: any;
  muscleGroups: string[];

  constructor() {
  }

  ngOnInit(): void {
    if (this.record) {
      this.original = { ...this.record };
    }

    this.muscleGroups = Object.keys(MuscleGroupsEnum).filter(x => !this.stringIsNumber(x));
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

  stringIsNumber(value: string): boolean {
    return isNaN(Number(value)) === false;
  }
}
