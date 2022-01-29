import { Component, Input, OnInit } from '@angular/core';
import { GymRecordDto } from './gymRecordDto';

@Component({
  selector: 'app-gym-record-item',
  templateUrl: './gym-record-item.component.html',
  styleUrls: ['./gym-record-item.component.css']
})
export class GymRecordItemComponent implements OnInit {

  @Input() record: GymRecordDto;

  constructor() { }

  ngOnInit(): void {
  }
}
