import { formatDate } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MeasurementDto } from '../measurement-item/measurementDto';

@Component({
  selector: 'app-add-measurement',
  templateUrl: './add-measurement.component.html',
  styleUrls: ['./add-measurement.component.css']
})
export class AddMeasurementComponent implements OnInit {

  @Input() measurement: MeasurementDto;
  @Output() onChangeMeasurement: EventEmitter<MeasurementDto> = new EventEmitter<MeasurementDto>();
  original: any;

  constructor() {
  }

  ngOnInit(): void {
    if (this.measurement) {
      this.original = { ...this.measurement };
    }
  }

  onChange(): void {
    if (this.hasChanged()) {
      this.onChangeMeasurement.emit(this.measurement);
    }
  }

  getDate(): string {
    return formatDate(this.measurement.measurementDate, 'yyyy-MM-dd', 'en-us');
  }

  setDate(event: any): void {
    this.measurement.measurementDate = new Date(event.target.value);
  }

  hasChanged(): boolean {
    if (this.original) {
      let keys = Object.keys(this.original);
      let asAny = this.measurement as any;
      for (let i = 0; i < keys.length; i++) {
        let prop = keys[i];
        if (this.original[prop] !== asAny[prop]) {
          return true;
        }
      }
    }
    return false;
  }
}
