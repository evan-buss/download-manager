import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  Validators,
  AbstractControl,
} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styles: [],
})
export class LoginPageComponent implements OnInit {
  loginFailed = false;
  loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(8)]],
  });

  get username(): AbstractControl {
    return this.loginForm.controls.username;
  }

  get password(): AbstractControl {
    return this.loginForm.controls.password;
  }

  constructor(private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void { }

  onSubmit() {
    console.log(this.loginForm.value);
    this.router.navigateByUrl("/dashboard");
  }
}
