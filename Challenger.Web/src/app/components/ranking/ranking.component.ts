import { Component, OnInit } from '@angular/core';
import { RankingService } from 'src/app/services/ranking.service';
import { UserScoresDto } from './UserScoresDto';

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.css']
})
export class RankingComponent implements OnInit {

  scoresChartOptions: any;
  usersScores: UserScoresDto[];

  constructor(private rankingService: RankingService) { }

  ngOnInit(): void {
    this.getUsersScores();
  }

  getUsersScores() {
    this.rankingService.getUsersScores().subscribe(
      (scores) => {
        this.usersScores = scores;
        this.scoresChartOptions = this.setOptions();
      });
  }

  setOptions() {
    return {
      title: {
        text: 'BECON SCORE CHART',
      },
      legend: {
        data: this.getLegend()
      },
      tooltip: {
      },
      xAxis: {
        name: 'Date',
        nameLocation: 'middle',
        nameTextStyle: { fontSize: '18', lineHeight: 56 },
        type: 'time',
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
