import { Component, OnInit } from '@angular/core';
import { MeasurementService } from 'src/app/services/measurement.service';
import { UserService } from 'src/app/services/user.service';
import { MeasurementDto } from '../measurement-item/measurementDto';
import { MeasurementsChart } from './measurements.chart';
import { UserBasicDto } from './UserBasicDto';

@Component({
  selector: 'app-measurements',
  templateUrl: './measurements.component.html',
  styleUrls: ['./measurements.component.css']
})
export class MeasurementsComponent implements OnInit {

  userBasicData: UserBasicDto;
  measurements: MeasurementDto[];
  measurementsChartOptions: any;
  isAdding: boolean = false;
  isValid: boolean = true;
  measurementToAdd: MeasurementDto;

  constructor(
    private measurementService: MeasurementService,
    private userService: UserService,
    private chart: MeasurementsChart) { }

  ngOnInit(): void {
    this.getUserBasicData();
    this.getMeasurements();
  }

  getMeasurements(): void {
    this.measurementService.getMeasurements().subscribe(
      (measurements) => {
        this.measurements = measurements;
        this.sortByDate();
        this.measurementsChartOptions = this.chart.setOptions(this.measurements, this.userBasicData);
      });
  }

  getUserBasicData(): void {
    this.userService.getUserBasic().subscribe(
      (userData) => {
        this.userBasicData = userData;
      }
    );
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
        (data) => {
          this.sortByDate();
          this.measurementsChartOptions = this.chart.setOptions(this.measurements, this.userBasicData);
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
          this.measurementsChartOptions = this.chart.setOptions(this.measurements, this.userBasicData);
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