import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup

  constructor(
    readonly loginService: LoginService,
    readonly router: Router,
  ) {
    this.loginForm = new FormGroup({
      username: new FormControl(''),
      password: new FormControl(''),
    })
  }

  ngOnInit(): void {
  }

  performLogin() {
    this.loginForm.disable()
    const login = this.loginForm.value
    this.loginService.login(login).subscribe(
      result => {
        this.router.navigate(['/'])
      },
      error => {
        /** TODO toast */
        this.loginForm.enable()
      }
    )
  }

}
