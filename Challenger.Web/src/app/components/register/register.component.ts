import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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

  constructor(
    private accountService: AccountService,
    private router: Router) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      email: new FormControl("", [Validators.required, Validators.email]),
      nickname: new FormControl("", [Validators.required, Validators.minLength(3)]),
      password: new FormControl("", [Validators.required, Validators.pattern('^((?=.*[A-Z])(?=.*[!@#$&_*])(?=.*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8,16})$')]),
      confirmPassword: new FormControl("", [Validators.required]),
      height: new FormControl("", [Validators.required]),
      birthDate: new FormControl("", [Validators.required]),
      sex: new FormControl("", [Validators.required]),
    })
  }

  public validateControl = (controlName: string) => {
    return this.registerForm.controls[controlName].invalid && this.registerForm.controls[controlName].touched
  }

  public hasError = (controlName: string, errorName: string) => {    
    return this.registerForm.controls[controlName].hasError(errorName)
  }

  public confirmPassword(): boolean {
    return this.registerForm.controls['password'].value == this.registerForm.controls['confirmPassword'].value;
  }

  public confirmHeight(): boolean {
    if (this.registerForm.controls['height'].value) {
      const height = parseFloat(this.registerForm.controls['height'].value);
      return height >= 120 && height <= 240;
    }
    
    return true;
  }

  registerUser(registerFormValue: any): void {
    this.errorMessage = '';
    this.accountService.register(registerFormValue).subscribe(
      (result) => {
        if (result.isSuccess) {
          this.router.navigate(['']);
        }
        else {
          this.errorMessage = result.errors;
        }
      },
      (error) =>{
        this.errorMessage = error.status + ' - ' + error.statusText;
      })
  }
}
