import { Component, OnInit } from '@angular/core';
import { DateHelper } from 'src/app/helpers/DateHelper';
import { GymRecordService } from 'src/app/services/gym-record.service';
import { GymRecordDto } from '../gym-record-item/gymRecordDto';

@Component({
  selector: 'app-gym-records',
  templateUrl: './gym-records.component.html',
  styleUrls: ['./gym-records.component.css']
})
export class GymRecordsComponent implements OnInit {

  records: GymRecordDto[];
  isAdding: boolean = false;
  isValid: boolean = true;
  recordToAdd: GymRecordDto;

  constructor(private gymRecordService: GymRecordService, private dateHelper: DateHelper) { }

  ngOnInit(): void {
    this.getGymRecords();
  }

  getGymRecords(): void {
    this.gymRecordService.getGymRecords().subscribe(
      (records) => {
        this.records = records;
        this.sortByDate();
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
          this.sortByDate();
        }
      );
    }
  }

  onSave(): void {
    if (this.validate(this.recordToAdd)) {
      this.setDateAndTime();
      this.gymRecordService.addGymRecord(this.recordToAdd).subscribe(
        (record) => {
          this.records.push(record);
          this.sortByDate();
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
          this.sortByDate();
        }
      });
  }

  validate(record: GymRecordDto): boolean {

    if(!record.recordDate)
    {
      console.log("Co kurwa"+ record.recordDate);
    }

    return !(!record.recordDate || !record.excersize || !record.muscleGroup || !record.repetitions || !record.weight)
  }

  setDateAndTime(): void {

    let now = new Date();
    if (this.dateHelper.getDateOnlyAsNumber(now) == this.dateHelper.getDateOnlyAsNumber(this.recordToAdd.recordDate)) {
      this.recordToAdd.recordDate = now;
    }
  }

  sortByDate(): void {
    this.records.sort((a: GymRecordDto, b: GymRecordDto) => {
      let bDate = new Date(b.recordDate);
      let aDate = new Date(a.recordDate);
      return bDate.getTime() - aDate.getTime();
    })
  }
}
