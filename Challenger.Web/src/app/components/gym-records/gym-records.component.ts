import { Component, OnInit } from '@angular/core';
import { Enumerable } from 'src/app/helpers/Enumerable';
import { DateHelper } from 'src/app/helpers/DateHelper';
import { GymRecordService } from 'src/app/services/gym-record.service';
import { GymRecordDto } from '../gym-record-item/gymRecordDto';
import { Grouping } from 'src/app/helpers/Grouping';

@Component({
  selector: 'app-gym-records',
  templateUrl: './gym-records.component.html',
  styleUrls: ['./gym-records.component.css']
})
export class GymRecordsComponent implements OnInit {

  records: GymRecordDto[];
  groupedReocords: Grouping<GymRecordDto>;
  groupedCollapse: any = {};
  isAdding: boolean = false;
  isValid: boolean = true;
  recordToAdd: GymRecordDto;
  excersiseOptions: string[];

  constructor(
    private gymRecordService: GymRecordService,
    private dateHelper: DateHelper) { }

  ngOnInit(): void {
    this.getGymRecords();
  }

  getGymRecords(): void {
    this.gymRecordService.getGymRecords().subscribe(
      (records) => {
        this.records = records;
        this.excersiseOptions = Enumerable.distinct(records.map(x => x.excersize)).sort();
        this.records.forEach(x => this.convertDate(x));
        this.refreshRecords();
      });
  }

  addGymRecord(): void {
    this.isAdding = !this.isAdding;

    if (this.isAdding) {
      this.recordToAdd = new GymRecordDto();
    }
  }

  onCancell(): void {
    this.isAdding = false;
    this.recordToAdd = new GymRecordDto();
  }

  onChange(record: GymRecordDto): void {
    if (this.validate(record)) {
      this.gymRecordService.updateGymRecord(record).subscribe(
        (data) => {
          this.refreshRecords();
        }
      );
    }
  }

  onSave(): void {
    if (this.validate(this.recordToAdd)) {
      this.setDateAndTime();
      this.gymRecordService.addGymRecord(this.recordToAdd).subscribe(
        (record) => {
          this.convertDate(record);
          this.records.push(record);
          this.refreshRecords();
        })
    }
  }

  onDelete(id: number): void {

    let toDelete = this.records.filter(x => x.id == id)[0];
    this.gymRecordService.deleteGymRecord(toDelete).subscribe(
      (record) => {
        let index = this.records.indexOf(toDelete);
        if (index > -1) {
          this.records.splice(index, 1);
          this.refreshRecords();
        }
      });
  }

  onCollapse(key: string): void {
    this.groupedCollapse[key] = !this.groupedCollapse[key];
  }

  validate(record: GymRecordDto): boolean {

    return !(!record.recordDate || !record.excersize || !record.muscleGroup || !record.repetitions || !record.weight)
  }

  setDateAndTime(): void {

    let now = new Date();
    if (this.compareDates(now, this.recordToAdd.recordDate)) {
      this.recordToAdd.recordDate = now;
    }
  }

  private refreshRecords(): void {
    this.sortByDate();
    this.groupedReocords = Enumerable.groupBy(this.records, x => this.dateHelper.getDateOnlyAsNumber(x.recordDate).toString());
    this.groupedReocords.keys().forEach(x => {
      if (!this.groupedCollapse[x]) {
        this.groupedCollapse[x] = false;
      }
    });
  }

  private compareDates(left: Date, right: Date): boolean {
    return this.dateHelper.getDateOnlyAsNumber(left) == this.dateHelper.getDateOnlyAsNumber(right)
  }

  private convertDate(record: GymRecordDto): void {
    record.recordDate = new Date(record.recordDate);
  }

  private sortByDate(): void {
    this.records.sort((a: GymRecordDto, b: GymRecordDto) => {
      let bDate = new Date(b.recordDate);
      let aDate = new Date(a.recordDate);
      return bDate.getTime() - aDate.getTime();
    })
  }
}
