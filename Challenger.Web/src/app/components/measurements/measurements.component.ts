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
    this.isAdding = !this.isAdding;

    if (this.isAdding) {
      this.measurementToAdd = new MeasurementDto();
    }
  }

  onCancell(): void {
    this.isAdding = false;
    this.measurementToAdd = new MeasurementDto();
  }

  onChange(measurement: MeasurementDto): void {
    if (this.validate(measurement)) {
      this.measurementService.updateMeasurement(measurement).subscribe(
        (data) =>{
          this.sortByDate();
        }
      );
    }
  }

  onSave(): void {
    if (this.validate(this.measurementToAdd)) {
      this.measurementService.addMeasurement(this.measurementToAdd).subscribe(
        (measurement) => {
          this.measurements.push(measurement);
          this.sortByDate();
        });
    }
  }

  onDelete(id: number): void {

    let toDelete = this.measurements.filter(x => x.id == id)[0];
    this.measurementService.deleteMeasurement(toDelete).subscribe(
      (measurement) => {
        let index = this.measurements.indexOf(toDelete);
        if (index > -1) {
          this.measurements.splice(index, 1);
          this.sortByDate();
        }
      });
  }

  validate(measurement: MeasurementDto): boolean {
    return !(!measurement.measurementDate || !measurement.weight || measurement.weight == 0)
  }

  sortByDate(): void {
    this.measurements.sort((a: MeasurementDto, b: MeasurementDto) => {
      let bDate = new Date(b.measurementDate);
      let aDate = new Date(a.measurementDate);
      return bDate.getTime() - aDate.getTime();
    })
  }
}
