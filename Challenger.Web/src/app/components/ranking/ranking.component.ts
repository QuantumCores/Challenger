import { Component, OnInit } from '@angular/core';
import { RankingService } from 'src/app/services/ranking.service';
import { RankingChart } from './ranking.chart';
import { UserScoresDto } from './UserScoresDto';

@Component({
  selector: 'app-ranking',
  templateUrl: './ranking.component.html',
  styleUrls: ['./ranking.component.css']
})
export class RankingComponent implements OnInit {

  scoresChartOptions: any;
  usersScores: UserScoresDto[];

  constructor(
    private rankingService: RankingService,
    private chart: RankingChart) { }

  ngOnInit(): void {
    this.getUsersScores();
  }

  getUsersScores() {
    this.rankingService.getUsersScores().subscribe(
      (scores) => {
        this.usersScores = scores;
        this.scoresChartOptions = this.chart.setOptions(this.usersScores);
      });
  }  
}
