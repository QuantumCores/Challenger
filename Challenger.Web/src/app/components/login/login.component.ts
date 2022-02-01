import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  errorMessage: string = '';
  showError: boolean;

  constructor(
    private accountService: AccountService,
    private jwtHelper: JwtHelperService,
    private router: Router) { }

  ngOnInit(): void {

    this.loginForm = new FormGroup({
      email: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required])
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
    this.accountService.login(registerFormValue).subscribe(
      (result) => {
        if (result.isSuccess) {
          localStorage.setItem("jwt", result.token);
        }
        else {

          this.errorMessage = 'Unable to login';
          this.showError = true;
        }
      })
  }

  logoutUser() {
    localStorage.removeItem("jwt");
    this.router.navigate(['']);
  }

  isUserAuthenticated(): boolean {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }

    return false;
  }
}
