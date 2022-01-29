import { Component, OnInit } from '@angular/core';
import { MeasurementService } from 'src/app/services/measurement.service';
import { MeasurementDto } from '../measurement-item/measurementDto';

@Component({
  selector: 'app-measurements',
  templateUrl: './measurements.component.html',
  styleUrls: ['./measurements.component.css']
})
export class MeasurementsComponent implements OnInit {

  measurements: MeasurementDto[];
  isAdding: boolean = false;
  isValid: boolean = true;
  measurementToAdd: MeasurementDto;

  constructor(private measurementService: MeasurementService) { }

  ngOnInit(): void {
    this.getMeasurements();
  }

  getMeasurements(): void {
    this.measurementService.getMeasurements().subscribe(
      (measurements) => {
        this.measurements = measurements;
        this.sortByDate();
      });

  }

  addMeasurement(): void {
    this.isAdding = true;
    this.measurementToAdd = new MeasurementDto();
  }

  onCancell(): void {
    this.isAdding = false;
    this.measurementToAdd = new MeasurementDto();
  }

  onSave(): void {
    let cloned = { ...this.measurementToAdd };
    if (this.validate()) {
      this.measurementService.addMeasurement(cloned).subscribe(
        (measurement) => (this.measurements.push(measurement)));
    }

    this.sortByDate();
  }

  validate(): boolean {
    return !(!this.measurementToAdd.measurementDate || !this.measurementToAdd.weight)
  }

  sortByDate(): void {
    this.measurements.sort((a: MeasurementDto, b: MeasurementDto) => {
      let bDate = new Date(b.measurementDate);
      let aDate = new Date(a.measurementDate);
      return bDate.getTime() - aDate.getTime();
    })
  }
}
