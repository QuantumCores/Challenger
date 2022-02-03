import { Component, OnInit } from '@angular/core';
import { AccountHelper } from 'src/app/helpers/AccountHelper';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  title = 'Challenger';
  
  constructor(private accountHelper: AccountHelper) { }

  ngOnInit(): void {
  }

  isUserAuthenticated(): boolean {    
    return this.accountHelper.isUserAuthenticated();
  }
}
