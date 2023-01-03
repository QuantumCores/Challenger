import { Injectable } from "@angular/core";
import { Enumerable } from "src/app/helpers/Enumerable";
import { DiaryRecordDto } from "../diary-add-record/DiaryRecordDto";

@Injectable({
  providedIn: 'root'
})
export class DiaryRecordChart {

  private records: DiaryRecordDto[];

  constructor() {
  }

  setOptions(reocrds: DiaryRecordDto[]) {
    this.records = reocrds;

    return {
      grid: {
        left: 10,
        containLabel: true,
        bottom: 10,
        top: 10,
        right: 30
      },
      tooltip: {
        trigger: 'axis',
        axisPointer: {
          type: 'cross'
        }
      },
      xAxis: {
        type: 'category',
        data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
      },
      yAxis: {
        //name: 'Weight',
        //nameLocation: 'middle',
        //nameTextStyle: { fontSize: '18', lineHeight: 56 },
        //axisLine: { show: true, lineStyle: { color: '#5470C6' } },
        //axisTick: { show: true },
        type: 'value',
        //alignTicks: true,
        //min: function (value: any): number { return Math.floor(value.min / 2) * 2; },
        axisLabel: {
          formatter: '{value} kCal'
        }
      },
      series: this.getCaloriesSeries(),
    };
  }

  private getCaloriesSeries(): any[] {

    const carbohydrates: number[] = this.getData(this.records, b => b.carbohydrates, 4);
    const proteins: number[] = this.getData(this.records, b => b.proteins, 4);
    const fats: number[] = this.getData(this.records, b => b.fats, 9);
    const energy: number[] = this.getData(this.records, b => b.energy, 1);

    // for (let i = 0; i < energy.length; i++) {
    //   energy[i] = energy[i] - (carbohydrates[i] + proteins[i] + fats[i]);
    // }


    return [{
      name: 'Carbohydrates',
      type: 'bar',
      stack: 'carbohydrates',
      emphasis: {
        focus: 'series'
      },
      data: carbohydrates
    },
    {
      name: 'Proteins',
      type: 'bar',
      stack: 'proteins',
      emphasis: {
        focus: 'series'
      },
      data: proteins
    },
    {
      name: 'Fats',
      type: 'bar',
      stack: 'fats',
      emphasis: {
        focus: 'series'
      },
      data: fats
    },
    {
      name: 'Energy',
      type: 'bar',
      stack: 'energy',
      // barGap: '-100%',
      // barCategoryGap: '10%',
      emphasis: {
        focus: 'series'
      },
      data: energy
    }];
  }

  private getData(records: DiaryRecordDto[], selector: (model: any) => number, gramToCalMultiplier: number): number[] {

    return records.map(x => gramToCalMultiplier * x.mealRecords.reduce((y, z) => y
      + z.mealProducts.reduce((a, b) => a + selector(b), 0)
      + z.fastRecords.reduce((a, b) => a + selector(b), 0)
      + z.mealDishes.reduce((a, b) => a + selector(b), 0)
      , 0));
  }
}