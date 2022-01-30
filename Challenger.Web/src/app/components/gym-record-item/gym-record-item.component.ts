import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GymRecordDto } from './gymRecordDto';

@Component({
  selector: 'app-gym-record-item',
  templateUrl: './gym-record-item.component.html',
  styleUrls: ['./gym-record-item.component.css']
})
export class GymRecordItemComponent implements OnInit {

  @Input() record: GymRecordDto;
  @Output() onChangeGymRecord: EventEmitter<GymRecordDto> = new EventEmitter<GymRecordDto>();
  @Output() onDeleteGymRecord: EventEmitter<number> = new EventEmitter<number>();
  
  isSelected: boolean = false;

  constructor() {
  }

  ngOnInit(): void {
  }

  onSelect(): void {
    this.isSelected = !this.isSelected;
  }

  onChange(): void {    
      this.onChangeGymRecord.emit(this.record);   
  }

  onDelete(id:number): void {
      this.onDeleteGymRecord.emit(id);
  }
}
