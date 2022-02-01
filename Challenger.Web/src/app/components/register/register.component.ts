import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  showError: boolean;
  errorMessage: string;

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      email: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required]),
      confirmPassword: new FormControl("", [Validators.required]),
      height: new FormControl("", [Validators.required]),
      birthDate: new FormControl("", [Validators.required]),
    })
  }

  public validateControl = (controlName: string) => {
    return this.registerForm.controls[controlName].invalid && this.registerForm.controls[controlName].touched
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.registerForm.controls[controlName].hasError(errorName)
  }

  registerUser(registerFormValue: any): void {

    let registerModel = registerFormValue;
    this.accountService.register(registerFormValue).subscribe(
      (result) => {
        this.errorMessage = result.errors;
      })
  }
}
