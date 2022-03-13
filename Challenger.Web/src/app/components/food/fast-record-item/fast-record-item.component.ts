import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FastRecordService } from 'src/app/services/fast-record.service';
import { FastRecordDto } from '../fast-record-add/FastRecordDto';

@Component({
  selector: 'app-fast-record-item',
  templateUrl: './fast-record-item.component.html',
  styleUrls: ['./fast-record-item.component.css']
})
export class FastRecordItemComponent implements OnInit {

  errorMessage: string;
  
  @Input() record : FastRecordDto;
  @Output() onDeleteFastRecord: EventEmitter<FastRecordDto> = new EventEmitter<FastRecordDto>();

  constructor(private fastReocrdService: FastRecordService) { }

  ngOnInit(): void {
  }

  onDelete(): void {
    this.errorMessage = '';
    this.fastReocrdService.deleteFastRecord(this.record.id).subscribe(
      {
        next: () => {
          this.onDeleteFastRecord.emit(this.record);
        },
        error: (error: any) => {
          this.errorMessage = error.status + ' - ' + error.statusText;
        }
      });
  }
}
