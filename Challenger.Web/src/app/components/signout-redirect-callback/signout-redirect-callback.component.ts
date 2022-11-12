import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-signout-redirect-callback',
  template: '<div></div>',
})
export class SignoutRedirectCallbackComponent implements OnInit {

  constructor(
    private _accountService: AccountService,
    private _router: Router
  ) { }

  ngOnInit(): void {
    this._accountService.finishLogout()
      .then(_ => {
        this._router.navigate(['/'], { replaceUrl: true });
      })
  }

}
