import { Component, OnInit } from '@angular/core';
import { DateHelper } from 'src/app/helpers/DateHelper';
import { DiaryRecordService } from 'src/app/services/diary-record.service';
import { MealRecordService } from 'src/app/services/meal-record.service';
import { DiaryRecordDto } from '../diary-add-record/DiaryRecordDto';
import { MealRecordDto } from '../meal-add-record/MealRecordDto';
import { DaySummary } from './DaySummary';
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
  summary: DaySummary;
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
      this.mealRecordToAdd.mealProducts = [];
      this.mealRecordToAdd.mealDishes = [];
      this.mealRecordToAdd.fastRecords = [];
      this.mealRecordToAdd.isNextDay = false;
    }
  }

  onStart() {
    let record = this.records[this.currentWeekDay - 1];
    if (!record.id || record.id == 0) {
      this.addNewDiaryRecord(record);
    }
  }

  onSave() {
    this.errorMessage = '';
    if (this.validate(this.mealRecordToAdd)) {

      let record = this.records[this.currentWeekDay - 1];

      if (!record.id || record.id == 0) {
        record.mealRecords.push(this.mealRecordToAdd);
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

  reloadChart(): void {
    this.summary = this.recalculate();
    this.diaryRecordsChartOptions = this.chart.setOptions(this.records);
  }

  private recalculate(): DaySummary {
    let summary = new DaySummary();

    summary.energy = this.records.reduce((v, x) => v + x.mealRecords.reduce((y, z) => y
      + z.mealProducts.reduce((a, b) => a + b.energy, 0)
      + z.fastRecords.reduce((a, b) => a + b.energy, 0)
      + z.mealDishes.reduce((a, b) => a + b.energy, 0)
      , 0), 0);

    summary.carbohydrates = this.records.reduce((v, x) => v + x.mealRecords.reduce((y, z) => y
      + z.mealProducts.reduce((a, b) => a + b.carbohydrates, 0)
      + z.fastRecords.reduce((a, b) => a + b.carbohydrates, 0)
      + z.mealDishes.reduce((a, b) => a + b.carbohydrates, 0)
      , 0), 0);

    summary.proteins = this.records.reduce((v, x) => v + x.mealRecords.reduce((y, z) => y
      + z.mealProducts.reduce((a, b) => a + b.proteins, 0)
      + z.fastRecords.reduce((a, b) => a + b.proteins, 0)
      + z.mealDishes.reduce((a, b) => a + b.proteins, 0)
      , 0), 0);

    summary.fats = this.records.reduce((v, x) => v + x.mealRecords.reduce((y, z) => y
      + z.mealProducts.reduce((a, b) => a + b.fats, 0)
      + z.fastRecords.reduce((a, b) => a + b.fats, 0)
      + z.mealDishes.reduce((a, b) => a + b.fats, 0)
      , 0), 0);

    return summary;
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
          record.diaryDate = new Date(record.diaryDate);
          const weekDay = this.dateHelper.getWeekDay(record.diaryDate);
          this.records[weekDay - 1] = record;
          this.isAdding = false;
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
          this.records[this.currentWeekDay - 1].mealRecords.push(record);
          this.diaryRecordsChartOptions = this.chart.setOptions(this.records);
          this.isAdding = false;
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
        this.reloadChart();
      });
  }

  private prepareRecords(): void {
    this.records = [];
    for (let i = 0; i < this.daysInWeek; i++) {
      let recordDate = this.dateHelper.getDateOnly(new Date(this.weekStartDate.getTime() + i * this.milisecondsInDay));
      this.records.push(this.getEmptyRecord(recordDate));
    }
  }

  private updateRecords(records: DiaryRecordDto[]): void {

    records.forEach(x => x.diaryDate = this.dateHelper.getDateOnly(new Date(x.diaryDate)));
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
