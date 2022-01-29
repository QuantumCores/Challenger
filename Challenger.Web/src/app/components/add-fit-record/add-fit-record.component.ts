import { Component, Input, OnInit } from '@angular/core';
import { FitRecordDto } from '../fit-record-item/fitRecordDto';

@Component({
  selector: 'app-add-fit-record',
  templateUrl: './add-fit-record.component.html',
  styleUrls: ['./add-fit-record.component.css']
})
export class AddFitRecordComponent implements OnInit {

  @Input() record: FitRecordDto = {
    Id: 1,
    UserId: 23,
    RecordDate: new Date(2022, 1, 23),
    Excersize: 'Ławka płasko',
    Repetitions: 8,
    Duration: 11,
    Distance: 3,
  };

  constructor() { }

  ngOnInit(): void {
  }

}
