import { Injectable } from "@angular/core";
import { CalculationProvider } from "src/app/providers/CalculationProvider";
import { MeasurementDto } from "../measurement-item/measurementDto";
import { UserBasicDto } from "./UserBasicDto";

@Injectable({
    providedIn: 'root'
})
export class MeasurementsChart {

    private measurements: MeasurementDto[];
    private userBasicData: UserBasicDto;

    constructor(
        private calculationProvider: CalculationProvider) {
    }

    setOptions(measurements: MeasurementDto[], userBasicData: UserBasicDto) {
        this.measurements = measurements;
        this.userBasicData = userBasicData;
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
                min: function (value: any): number { return Math.floor(value.min / 2) * 2; },
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
                min: function (value: any): number { return Math.floor(value.min / 2) * 2; },
                axisLabel: {
                    formatter: '{value} %'
                }
            },
            ],
            series: this.getFatAndWeightSeries(),
        };
    }

    private getFatAndWeightSeries(): any[] {
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
                const calcFat = this.calculationProvider.calculateFatNavyFormula(this.userBasicData.sex == 'male', element.waist, element.neck, this.userBasicData.height, 0);
                fatCalcData.push({ value: [new Date(element.measurementDate), calcFat] });
            }
        }
        series.push({ name: 'weight', type: 'line', data: weightData });
        series.push({ name: 'fat', yAxisIndex: 1, type: 'line', data: fatData });
        series.push({ name: 'calcFat', yAxisIndex: 1, type: 'line', data: fatCalcData });

        return series;
    }

    private getSeries(keysToPrint: string[]): any {
        let series: any[] = [];

        for (let i = 0; i < keysToPrint.length; i++) {
            const key = keysToPrint[i];
            series.push(this.getSerie(key));
        }

        return series;
    }

    private getSerie(name: string): any {
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

    private getLegend(): string[] {

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