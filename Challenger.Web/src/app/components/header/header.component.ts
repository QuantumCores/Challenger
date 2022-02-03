import { Component, OnInit } from '@angular/core';
import { AccountHelper } from 'src/app/helpers/AccountHelper';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private accountHelper: AccountHelper) { }

  ngOnInit(): void {
  } 
  
  isUserAuthenticated(): boolean {    
    return this.accountHelper.isUserAuthenticated();
  }
}
