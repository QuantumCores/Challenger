import { formatDate } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { MeasurementDto } from './measurementDto';

@Component({
  selector: 'app-measurement-item',
  templateUrl: './measurement-item.component.html',
  styleUrls: ['./measurement-item.component.css']
})
export class MeasurementItemComponent implements OnInit {

  @Input() measurement: MeasurementDto;
  dateString: string;
  constructor() {
  }

  ngOnInit(): void {
    this.measurement = {
      Id: 12,
      UserId: 23,
      MeasurementDate: new Date,
      Weight: 73,
      Waist: 90,
      Neck: 38,
      Chest: 100,
      Hips: 25,
      Biceps: 37,
      Tigh: 45,
      Calf: 23,
    };

    this.dateString = formatDate(this.measurement.MeasurementDate, 'yyyy-MM-dd', 'en-us');
  }

}
