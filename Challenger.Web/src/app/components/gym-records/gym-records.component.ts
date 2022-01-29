import { Component, OnInit } from '@angular/core';
import { GYMRECORDS } from 'src/app/mock-gymRecords';
import { GymRecordDto } from '../gym-record-item/gymRecordDto';

@Component({
  selector: 'app-gym-records',
  templateUrl: './gym-records.component.html',
  styleUrls: ['./gym-records.component.css']
})
export class GymRecordsComponent implements OnInit {

  records = GYMRECORDS;
  isAdding: boolean = false;
  isValid: boolean = true;
  recordToAdd: GymRecordDto;

  constructor() { }

  ngOnInit(): void {
    this.sortByDate();
  }

  addRecord(): void {
    this.isAdding = true;
    this.recordToAdd = new GymRecordDto();
  }

  onCancell(): void {
    this.isAdding = false;
    this.recordToAdd = new GymRecordDto();
  }

  onSave(): void {
    let cloned = {...this.recordToAdd};
    this.records.push(cloned);
    this.sortByDate();
  }

  validate(): boolean{
    return !(!this.recordToAdd.RecordDate || !this.recordToAdd.Weight) 
  }

  sortByDate():void{
    this.records.sort((a: GymRecordDto, b: GymRecordDto) => {
      let bDate = new Date(b.RecordDate);
      let aDate = new Date(a.RecordDate);
      return bDate.getTime() - aDate.getTime();
    })
  }
}
