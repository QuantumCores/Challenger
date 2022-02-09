import { Component, OnInit } from '@angular/core';
import { SeriesModel } from 'echarts';
import { CalculationProvider } from 'src/app/providers/CalculationProvider';
import { MeasurementService } from 'src/app/services/measurement.service';
import { UserService } from 'src/app/services/user.service';
import { MeasurementDto } from '../measurement-item/measurementDto';
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
    private calculationProvider: CalculationProvider) { }

  ngOnInit(): void {
    this.getUserBasicData();
    this.getMeasurements();
  }

  getMeasurements(): void {
    this.measurementService.getMeasurements().subscribe(
      (measurements) => {
        this.measurements = measurements;
        this.sortByDate();
        this.measurementsChartOptions = this.setOptions();
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
          this.measurementsChartOptions = this.setOptions();
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
          this.measurementsChartOptions = this.setOptions();
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

  setOptions() {
    const keysToPrint: string[] = this.getLegend();

    return {
      legend: {
        data: keysToPrint,
        bottom: 0,
        textStyle: { fontSize: 16, padding: 5 },
        itemGap: 20,
        height: 150,
        icon: 'roundRect',
        selector: false
      },
      tooltip: {
        trigger: 'axis',
        axisPointer: {
          type: 'cross'
        }
      },
      xAxis: {
        name: 'Date',
        nameLocation: 'middle',
        nameTextStyle: { fontSize: '18', lineHeight: 46 },
        type: 'time',
        min: '2022-02-05'
      },
      yAxis: [{
        name: 'Weight',
        //nameLocation: 'middle',
        nameTextStyle: { fontSize: '18', lineHeight: 56 },
        axisLine: { show: true, lineStyle: { color: '#5470C6' } },
        axisTick: { show: true },
        type: 'value',
        alignTicks: true,
        min: 40,
        axisLabel: {
          formatter: '{value} kg'
        }
      },
      {
        name: 'Fat',
        //nameLocation: 'middle',
        nameTextStyle: { fontSize: '18', lineHeight: 56 },
        axisLine: { show: true },
        axisTick: { show: true },
        type: 'value',
        alignTicks: true,
        min: 10,
        axisLabel: {
          formatter: '{value} %'
        }
      },
      ],
      series: this.getFatAndWeightSeries(),
    };
  }

  getFatAndWeightSeries(): any[] {
    let series: any[] = [];

    let weightData: any[] = [];
    let fatData: any[] = [];
    let fatCalcData: any[] = [];
    for (let i = 0; i < this.measurements.length; i++) {
      const element = this.measurements[i];
      weightData.push({ value: [new Date(element.measurementDate), element.weight] });

      if (element.fat) {
        fatData.push({ value: [new Date(element.measurementDate), element.fat] });
      }

      if (element.waist && element.neck) {
        const calcFat = this.calculationProvider.calculateFatNavyFormula(true, element.waist, element.neck, this.userBasicData.height, 0);
        fatCalcData.push({ value: [new Date(element.measurementDate), calcFat] });
      }
    }
    series.push({ name: 'weight', type: 'line', data: weightData });
    series.push({ name: 'fat', yAxisIndex: 1, type: 'line', data: fatData });
    series.push({ name: 'calcFat', yAxisIndex: 1, type: 'line', data: fatCalcData });

    return series;
  }

  getSeries(keysToPrint: string[]): any {
    let series: any[] = [];

    for (let i = 0; i < keysToPrint.length; i++) {
      const key = keysToPrint[i];
      series.push(this.getSerie(key));
    }

    return series;
  }

  getSerie(name: string): any {
    let data: any[] = [];
    for (let i = 0; i < this.measurements.length; i++) {
      const element = this.measurements[i];
      const asAny = element as any;
      if (asAny[name]) {
        data.push({ value: [new Date(element.measurementDate), asAny[name]] });
      }
    }

    return {
      name: name,
      type: 'line',
      data: data,
    };
  }

  sortByDate(): void {
    this.measurements.sort((a: MeasurementDto, b: MeasurementDto) => {
      let bDate = new Date(b.measurementDate);
      let aDate = new Date(a.measurementDate);
      return bDate.getTime() - aDate.getTime();
    })
  }

  getLegend(): string[] {

    return ['weight', 'fat', 'calcFat'];
    const keys = Object.keys(this.measurements[0]);
    let result: string[] = [];

    for (let i = 0; i < keys.length; i++) {
      const key = keys[i];
      if (key != 'id' && key != 'userId' && key != 'measurementDate') {
        result.push(key);
      }
    }

    return result;
  }
}
