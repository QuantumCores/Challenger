import { Component, OnInit } from '@angular/core';
import { DateHelper } from 'src/app/helpers/DateHelper';
import { DiaryRecordService } from 'src/app/services/diary-record.service';
import { MealRecordService } from 'src/app/services/meal-record.service';
import { DiaryRecordDto } from '../diary-add-record/DiaryRecordDto';
import { MealRecordDto } from '../meal-add-record/MealRecordDto';
import { DiaryRecordChart } from './diaryRecords.chart';

@Component({
  selector: 'app-diary-records',
  templateUrl: './diary-records.component.html',
  styleUrls: ['./diary-records.component.css']
})
export class DiaryRecordsComponent implements OnInit {

  private daysInWeek: number = 7;
  private milisecondsInDay: number = (1000 * 60 * 60 * 24);
  isAdding: boolean = false;
  isValid: boolean = true;
  weekStartDate: Date;
  currentWeekDay: number = 1;
  records: DiaryRecordDto[];
  diaryRecordsChartOptions: any;
  mealRecordToAdd: MealRecordDto;
  errorMessage: string;

  constructor(
    private diaryRecordService: DiaryRecordService,
    private mealRecordService: MealRecordService,
    private dateHelper: DateHelper,
    private chart: DiaryRecordChart) { }

  ngOnInit(): void {
    this.setCurrentWeekDay(new Date());
    this.setWeekStartDate(new Date());
    this.prepareRecords();
    this.getDiaryRecords();
  }

  addDiaryRecord(): void {
    this.isAdding = !this.isAdding;

    if (this.isAdding) {
      this.mealRecordToAdd = new MealRecordDto();
      this.mealRecordToAdd.dishes = [];
      this.mealRecordToAdd.mealProducts = [];
      this.mealRecordToAdd.isNextDay = false;
    }
  }

  onStart() {
    let record = this.records[this.currentWeekDay];
    if (!record.id || record.id == 0) {
      this.addNewDiaryRecord(record);
    }
  }

  onSave() {
    this.errorMessage = '';
    if (this.validate(this.mealRecordToAdd)) {

      let record = this.records[this.currentWeekDay];

      if (!record.id || record.id == 0) {
        this.addNewDiaryRecord(record);
      }
      else {
        this.mealRecordToAdd.diaryRecordId = record.id;
        this.addNewMealRecord();
      }
    }
  }

  setCurrentWeekDay(diaryDate: Date) {
    this.currentWeekDay = this.dateHelper.getWeekDay(diaryDate);
  }

  private setWeekStartDate(date: Date) {
    date = this.dateHelper.getDateOnly(date);
    if (this.currentWeekDay == 1) {
      this.weekStartDate = date;
    }
    else {
      this.weekStartDate = new Date(date.getTime() - (this.currentWeekDay - 1) * this.milisecondsInDay);
    }
  }

  private addNewDiaryRecord(record: DiaryRecordDto): void {
    this.diaryRecordService.addDiaryRecord(record).subscribe(
      {
        next: (record) => {
          this.records[this.currentWeekDay] = record;
        },
        error: (error) => {
          this.errorMessage = error.status + ' - ' + error.statusText;
        }
      });
  }

  private addNewMealRecord(): void {
    this.mealRecordService.addMealRecord(this.mealRecordToAdd).subscribe(
      {
        next: (record) => {
          this.records[this.currentWeekDay].mealRecords.push(record);
          this.diaryRecordsChartOptions = this.chart.setOptions(this.records);
        },
        error: (error) => {
          this.errorMessage = error.status + ' - ' + error.statusText;
        }
      }
    );
  }

  private validate(record: MealRecordDto): boolean {
    return !(!record.mealName || !record.recordTime)
  }

  private getDiaryRecords(): void {
    this.diaryRecordService.getDiaryRecords(this.weekStartDate).subscribe(
      (records) => {
        this.updateRecords(records);
        this.diaryRecordsChartOptions = this.chart.setOptions(this.records);
      });
  }

  private prepareRecords(): void {
    this.records = [];
    for (let i = 0; i < this.daysInWeek; i++) {
      let recordDate = new Date(this.weekStartDate.getTime() + i * this.milisecondsInDay);
      this.records.push(this.getEmptyRecord(recordDate));
    }
  }

  private updateRecords(records: DiaryRecordDto[]): void {

    records.forEach(x => x.diaryDate = new Date(x.diaryDate));
    this.dateHelper.sortByDateAscending(records, x => x.diaryDate, x => x.diaryDate);

    for (let i = 0; i < records.length; i++) {
      let record = records[i];
      let weekDay = this.dateHelper.getWeekDay(record.diaryDate);
      this.records[weekDay - 1] = records[i];
    }
  }

  private getEmptyRecord(recordDate: Date): DiaryRecordDto {
    return {
      diaryDate: recordDate,
      carbohydrates: 0,
      proteins: 0,
      fats: 0,
      mealRecords: [] as MealRecordDto[],
    } as DiaryRecordDto;
  }
}
