import { Component, OnInit } from '@angular/core';
import { DateHelper } from 'src/app/helpers/DateHelper';
import { FitRecordService } from 'src/app/services/fit-record.service';
import { FitRecordDto } from '../fit-record-item/fitRecordDto';

@Component({
  selector: 'app-fit-records',
  templateUrl: './fit-records.component.html',
  styleUrls: ['./fit-records.component.css']
})
export class FitRecordsComponent implements OnInit {

  records: FitRecordDto[];
  isAdding: boolean = false;
  isValid: boolean = true;
  recordToAdd: FitRecordDto;

  constructor(private fitRecordService: FitRecordService, private dateHelper: DateHelper) { }

  ngOnInit(): void {
    this.getFitRecords();
  }

  getFitRecords(): void {
    this.fitRecordService.getFitRecords().subscribe(
      (records) => {
        this.records = records;
        this.sortByDate();
      });

  }

  addFitRecord(): void {
    this.isAdding = !this.isAdding;

    if (this.isAdding) {
      this.recordToAdd = new FitRecordDto();
    }
  }

  onCancell(): void {
    this.isAdding = false;
    this.recordToAdd = new FitRecordDto();
  }

  onChange(record: FitRecordDto): void {
    if (this.validate(record)) {
      this.fitRecordService.updateFitRecord(record).subscribe(
        (data) => {
          this.sortByDate();
        }
      );
    }
  }

  onSave(): void {
    if (this.validate(this.recordToAdd)) {
      this.setDateAndTime();
      this.fitRecordService.addFitRecord(this.recordToAdd).subscribe(
        (record) => {
          this.records.push(record);
          this.sortByDate();
        })
    }
  }

  onDelete(id: number): void {

    let toDelete = this.records.filter(x => x.id == id)[0];
    this.fitRecordService.deleteFitRecord(toDelete).subscribe(
      (record) => {
        let index = this.records.indexOf(toDelete);
        if (index > -1) {
          this.records.splice(index, 1);
          this.sortByDate();
        }
      });
  }

  validate(record: FitRecordDto): boolean {
    return !(!record.recordDate || !record.excersize || !record.burntCalories)
  }

  setDateAndTime(): void {

    let now = new Date();
    if (this.dateHelper.getDateOnlyAsNumber(now) == this.dateHelper.getDateOnlyAsNumber(this.recordToAdd.recordDate)) {
      this.recordToAdd.recordDate = now;
    }
  }

  sortByDate(): void {
    this.records.sort((a: FitRecordDto, b: FitRecordDto) => {
      let bDate = new Date(b.recordDate);
      let aDate = new Date(a.recordDate);
      return bDate.getTime() - aDate.getTime();
    })
  }
}
