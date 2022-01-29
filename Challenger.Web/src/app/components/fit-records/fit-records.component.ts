import { Component, OnInit } from '@angular/core';
import { FITRECORDS } from 'src/app/mock-fitRecords';
import { FitRecordDto } from '../fit-record-item/fitRecordDto';

@Component({
  selector: 'app-fit-records',
  templateUrl: './fit-records.component.html',
  styleUrls: ['./fit-records.component.css']
})
export class FitRecordsComponent implements OnInit {

  records = FITRECORDS;
  isAdding: boolean = false;
  isValid: boolean = true;
  recordToAdd: FitRecordDto;

  constructor() { }

  ngOnInit(): void {
    this.sortByDate();
  }

  addRecord(): void {
    this.isAdding = true;
    this.recordToAdd = new FitRecordDto();
  }

  onCancell(): void {
    this.isAdding = false;
    this.recordToAdd = new FitRecordDto();
  }

  onSave(): void {
    let cloned = {...this.recordToAdd};
    this.records.push(cloned);
    this.sortByDate();
  }

  validate(): boolean{
    return !(!this.recordToAdd.RecordDate || !this.recordToAdd.Excersize) 
  }

  sortByDate():void{
    this.records.sort((a: FitRecordDto, b: FitRecordDto) => {
      let bDate = new Date(b.RecordDate);
      let aDate = new Date(a.RecordDate);
      return bDate.getTime() - aDate.getTime();
    })
  }

}
