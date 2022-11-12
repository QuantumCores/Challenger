import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';
//import { AccountHelper } from 'src/app/helpers/AccountHelper';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  errorMessage: string = '';
  showError: boolean;
  isUserAuthenticated: boolean = false;

  constructor(
    private accountService: AccountService,
    //private accountHelper: AccountHelper,
    private router: Router) { }

  ngOnInit(): void {

    this.loginForm = new FormGroup({
      email: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required])
    });

    this.accountService.loginChanged
      .subscribe(res => {
        this.isUserAuthenticated = res;
      });
  }

  public validateControl = (controlName: string) => {
    return this.loginForm.controls[controlName].invalid && this.loginForm.controls[controlName].touched
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.loginForm.controls[controlName].hasError(errorName)
  }

  loginUser(registerFormValue: any): void {

    let registerModel = registerFormValue;
    this.accountService.login();
    // this.accountService.login(registerFormValue).subscribe(
    //   (result) => {
    //     if (result.isSuccess) {
    //       localStorage.setItem("jwt", result.token);
    //       this.router.navigate(['']);
    //     }
    //     else {

    //       this.errorMessage = 'Unable to login';
    //       this.showError = true;
    //     }
    //   })
  }

  public logout = () => {
    this.accountService.logout();
  }

  logoutUser() {
    localStorage.removeItem("jwt");
    this.router.navigate(['']);
  }

  // isUserAuthenticated(): boolean {    
  //   return this.accountHelper.isUserAuthenticated();
  // }
}
