import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IngridientService } from 'src/app/services/ingridient.service';
import { IngridientDto } from '../ingridient-add/IngridientDto';

@Component({
  selector: 'app-ingridient-item',
  templateUrl: './ingridient-item.component.html',
  styleUrls: ['./ingridient-item.component.css']
})
export class IngridientItemComponent implements OnInit {

  @Input() ingridient: IngridientDto;
  @Output() onDeleteIngridient: EventEmitter<IngridientDto> = new EventEmitter<IngridientDto>();

  errorMessage: string;

  constructor(private ingridientService: IngridientService) { }

  ngOnInit(): void {
  }

  onUpdate(): void {

  }

  onDelete(): void {
    this.errorMessage = '';
    this.onDeleteIngridient.emit(this.ingridient);
    // this.ingridientService.deleteIngridient(this.ingridient.id).subscribe(
    //   {
    //     next: () => {
    //       this.onDeleteIngridient.emit(this.ingridient);
    //     },
    //     error: (error: any) => {
    //       this.errorMessage = error.status + ' - ' + error.statusText;
    //     }
    //   });
  }
}
