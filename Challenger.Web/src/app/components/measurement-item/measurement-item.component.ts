import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MeasurementDto } from './measurementDto';

@Component({
  selector: 'app-measurement-item',
  templateUrl: './measurement-item.component.html',
  styleUrls: ['./measurement-item.component.css']
})
export class MeasurementItemComponent implements OnInit {

  @Input() measurement: MeasurementDto;
  @Output() onChangeMeasurement: EventEmitter<MeasurementDto> = new EventEmitter<MeasurementDto>();
  @Output() onDeleteMeasurement: EventEmitter<number> = new EventEmitter<number>();
  
  isSelected: boolean = false;

  constructor() {
  }

  ngOnInit(): void {
  }

  onSelect(): void {
    this.isSelected = !this.isSelected;
  }

  onChange(): void {    
      this.onChangeMeasurement.emit(this.measurement);   
  }

  onDelete(id:number): void {
      this.onDeleteMeasurement.emit(id);
  }
}
