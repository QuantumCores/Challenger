import { Component, Input, OnInit } from '@angular/core';
import { MealRecordDto } from './MealRecordDto';

@Component({
  selector: 'app-meal-add-record',
  templateUrl: './meal-add-record.component.html',
  styleUrls: ['./meal-add-record.component.css']
})
export class MealAddRecordComponent implements OnInit {

  @Input() record: MealRecordDto;
  options: string[] = ['Breakfast', 'Lunch', 'Dinner', 'Snack'];
  defaultValue = new Date();

  constructor() { }

  ngOnInit(): void {
    this.record.recordTime = this.defaultValue.getHours() * 60 + this.defaultValue.getMinutes();
  }

  onTimeChange(date: Date): void {
    this.record.recordTime = this.record.isNextDay ? (date.getHours() + 24) * 60 + date.getMinutes() : date.getHours() * 60 + date.getMinutes();
  }
}
