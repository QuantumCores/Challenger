import { formatDate } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { MeasurementDto } from '../measurement-item/measurementDto';

@Component({
  selector: 'app-add-measurement',
  templateUrl: './add-measurement.component.html',
  styleUrls: ['./add-measurement.component.css']
})
export class AddMeasurementComponent implements OnInit {

  @Input() measurement: MeasurementDto;
  currentDate: Date = new Date;

  constructor() {

  }

  ngOnInit(): void {
  }

  getDate(): string {
   return formatDate(this.measurement.measurementDate , 'yyyy-MM-dd', 'en-us');
  }

  setDate(event: any): void {
    this.measurement.measurementDate = new Date(event.target.value);
  }
}
