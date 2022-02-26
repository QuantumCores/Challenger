import { Component, OnInit } from '@angular/core';
import { RulesService } from 'src/app/services/rules.service';
import { RulesDto } from './RulesDto';

@Component({
  selector: 'app-challenge-rules',
  templateUrl: './challenge-rules.component.html',
  styleUrls: ['./challenge-rules.component.css']
})
export class ChallengeRulesComponent implements OnInit {

  rules: RulesDto;
  constructor(private rulesService: RulesService) { }

  ngOnInit(): void {
    this.getRules();
  }

  getRules(): void {
    this.rulesService.getRules().subscribe(
      (rules) => {
        this.rules = rules;
        this.rules.startDate = new Date(this.rules.startDate);
        this.rules.endDate = new Date(this.rules.endDate);
      });
  }

}
