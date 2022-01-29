import { Component, Input, OnInit } from '@angular/core';
import { GymRecordDto } from '../gym-record-item/gymRecordDto';

@Component({
  selector: 'app-add-gym-record',
  templateUrl: './add-gym-record.component.html',
  styleUrls: ['./add-gym-record.component.css']
})
export class AddGymRecordComponent implements OnInit {

  @Input() record: GymRecordDto = {
    Id: 1,
    UserId: 23,
    RecordDate: new Date(2022, 1, 23),
    Excersize: 'Ławka płasko',
    Weight: 80,
    Repetitions: 8,
    MuscleGroup: 'Chest'
  };

  constructor() { }

  ngOnInit(): void {
  }

}
