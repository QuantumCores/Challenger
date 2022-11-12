import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  isUserAuthenticated: boolean = false;
  title = 'Challenger';
  
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.loginChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      });
  }
}
