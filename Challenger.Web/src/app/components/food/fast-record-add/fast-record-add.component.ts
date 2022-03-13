import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FastRecordService } from 'src/app/services/fast-record.service';
import { FastRecordDto } from './FastRecordDto';

@Component({
  selector: 'app-fast-record-add',
  templateUrl: './fast-record-add.component.html',
  styleUrls: ['./fast-record-add.component.css']
})
export class FastRecordAddComponent implements OnInit {

  addFastRecordForm: FormGroup;
  errorMessage: string;

  @Input() mealRecordId: number;
  @Output() onFastRecordAdded: EventEmitter<FastRecordDto> = new EventEmitter<FastRecordDto>();

  constructor(private fastRecordService: FastRecordService) { }

  ngOnInit(): void {
    this.addFastRecordForm = new FormGroup({
      comment: new FormControl("", [Validators.minLength(3), Validators.maxLength(64)]),      
      energy: new FormControl("", [Validators.required]),
      fats: new FormControl("", [Validators.required]),
      proteins: new FormControl("", [Validators.required]),
      carbohydrates: new FormControl("", [Validators.required]),
    });
  }

  public validateControl = (controlName: string) => {
    return this.addFastRecordForm.controls[controlName].invalid && this.addFastRecordForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.addFastRecordForm.controls[controlName].hasError(errorName)
  }

  submit(fastRecordToAdd: FastRecordDto): void {
    this.errorMessage = '';
    fastRecordToAdd.mealRecordId = this.mealRecordId;
    this.fastRecordService.addFastRecord(fastRecordToAdd).subscribe(
      {
        next: (fastRecord) => {
          this.onFastRecordAdded.emit(fastRecord);
        },
        error: (error) => {
          this.errorMessage = error.status + ' - ' + error.statusText;
        }
      })
  }
}
