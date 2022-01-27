import { Component, Input, OnInit } from '@angular/core';
import { MeasurementDto } from '../measurement-item/measurementDto';

@Component({
  selector: 'app-add-measurement',
  templateUrl: './add-measurement.component.html',
  styleUrls: ['./add-measurement.component.css']
})
export class AddMeasurementComponent implements OnInit {

  measurement: MeasurementDto = {
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
  currentDate: Date = new Date;

  constructor() {

  }

  ngOnInit(): void {
  }

}
