import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isUserAuthenticated: boolean = false;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.loginChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      });
  }
}
