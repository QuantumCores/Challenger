import { Component, Input, OnInit } from '@angular/core';
import { FitRecordDto } from './fitRecordDto';

@Component({
  selector: 'app-fit-record-item',
  templateUrl: './fit-record-item.component.html',
  styleUrls: ['./fit-record-item.component.css']
})
export class FitRecordItemComponent implements OnInit {

  @Input() record: FitRecordDto;

  constructor() { }

  ngOnInit(): void {
  }

}
