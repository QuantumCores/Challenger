import { Component, Input, OnInit } from '@angular/core';
import { MeasurementDto } from './measurementDto';

@Component({
  selector: 'app-measurement-item',
  templateUrl: './measurement-item.component.html',
  styleUrls: ['./measurement-item.component.css']
})
export class MeasurementItemComponent implements OnInit {

  @Input() measurement: MeasurementDto;
  isSelected: boolean = false;

  constructor() {
  }

  ngOnInit(): void {
  }

  onSelect(): void {
    this.isSelected = !this.isSelected;
  }

  onClickDelete(): void {

  }
}
