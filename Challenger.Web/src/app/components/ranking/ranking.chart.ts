import { Injectable } from "@angular/core";
import { UserScoresDto } from "./UserScoresDto";

@Injectable({
    providedIn: 'root'
})
export class RankingChart {

    private usersScores: UserScoresDto[];

    constructor() {
    }

    setOptions(usersScores: UserScoresDto[]) {
        this.usersScores = usersScores;

        return {
            title: {
                text: 'BECON SCORE',
                textAlign: 'left',
                textStyle: { fontSize: 20, lineHeight: 56 }
            },
            legend: {
                data: this.getLegend(),
                bottom: 0,
                textStyle: { fontSize: 16, padding: 5 },
                itemGap: 20,
                height: 150,
                icon: 'roundRect',
                selector: false
            },
            tooltip: {
            },
            xAxis: {
                name: 'Date',
                nameLocation: 'middle',
                nameTextStyle: { fontSize: '18', lineHeight: 56 },
                type: 'time',
                min: '2022-02-05'
            },
            yAxis: {
                name: 'Score',
                nameLocation: 'middle',
                nameTextStyle: { fontSize: '18', lineHeight: 56 },
                axisLine: { show: true },
                axisTick: { show: true },
                type: 'value'
            },
            series: this.getSeries(),
        };
    }

    getSeries(): any[] {
        return this.usersScores.map(x => {
            return {
                name: x.userName,
                type: 'line',
                data: x.scores.map(y => { return { name: x.userName, value: [new Date(y.date), y.fullScore] }; }),
            }
        })
    }

    getLegend(): string[] {
        return this.usersScores.map(x => x.userName);
    }
}