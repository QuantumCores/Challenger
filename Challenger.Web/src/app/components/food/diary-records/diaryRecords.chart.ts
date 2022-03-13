import { Injectable } from "@angular/core";
import { Enumerable } from "src/app/helpers/Enumerable";
import { DiaryRecordDto } from "../diary-add-record/DiaryRecordDto";

@Injectable({
  providedIn: 'root'
})
export class DiaryRecordChart {

  private reocrds: DiaryRecordDto[];

  constructor() {
  }

  setOptions(reocrds: DiaryRecordDto[]) {
    this.reocrds = reocrds;

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

    return [{
      name: 'Carbohydrates',
      type: 'bar',
      stack: 'calories',
      emphasis: {
        focus: 'series'
      },
      data: this.reocrds.map(x => x.mealRecords.reduce((y,z) => y 
          + z.mealProducts.reduce((a,b) => a + b.carbohydrates, 0)
          + z.fastRecords.reduce((a,b) => a + b.carbohydrates, 0)
          + z.mealDishes.reduce((a,b) => a + b.carbohydrates, 0)
            , 0))
    },
    {
      name: 'Proteins',
      type: 'bar',
      stack: 'calories',
      emphasis: {
        focus: 'series'
      },
      data: this.reocrds.map(x => x.mealRecords.reduce((y,z) => y 
          + z.mealProducts.reduce((a,b) => a + b.proteins, 0)
          + z.fastRecords.reduce((a,b) => a + b.carbohydrates, 0)
          + z.mealDishes.reduce((a,b) => a + b.carbohydrates, 0)
          , 0))
    },
    {
      name: 'Fats',
      type: 'bar',
      stack: 'calories',
      emphasis: {
        focus: 'series'
      },
      data: this.reocrds.map(x => x.mealRecords.reduce((y,z) => y 
          + z.mealProducts.reduce((a,b) => a + b.fats, 0)
          + z.fastRecords.reduce((a,b) => a + b.carbohydrates, 0)
          + z.mealDishes.reduce((a,b) => a + b.carbohydrates, 0)
          , 0))
    }];
  }
}