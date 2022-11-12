import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-signin-redirect-callback',
  template: '<div></div>'
})
export class SigninRedirectCallbackComponent implements OnInit {

  constructor(
    private _accountService: AccountService,
    private _router: Router
  ) { }

  ngOnInit(): void {
    this._accountService.finishLogin()
      .then(_ => {
        this._router.navigate(['/'], { replaceUrl: true });
      })
  }

}
