import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FitRecordDto } from './fitRecordDto';

@Component({
  selector: 'app-fit-record-item',
  templateUrl: './fit-record-item.component.html',
  styleUrls: ['./fit-record-item.component.css']
})
export class FitRecordItemComponent implements OnInit {

  @Input() record: FitRecordDto;
  @Output() onChangeFitRecord: EventEmitter<FitRecordDto> = new EventEmitter<FitRecordDto>();
  @Output() onDeleteFitRecord: EventEmitter<number> = new EventEmitter<number>();
  
  isSelected: boolean = false;

  constructor() {
  }

  ngOnInit(): void {
  }

  onSelect(): void {
    this.isSelected = !this.isSelected;
  }

  onChange(): void {    
      this.onChangeFitRecord.emit(this.record);   
  }

  onDelete(id:number): void {
      this.onDeleteFitRecord.emit(id);
  }
}
