import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NotificationsService } from '../notifications/notifications.service';
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
    readonly notifications: NotificationsService
  ) {
    this.loginForm = new FormGroup({
      username: new FormControl('', [
        Validators.required
      ]),
      password: new FormControl('', [
        Validators.required
      ]),
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
        this.notifications.show('error', error)
        this.loginForm.enable()
      }
    )
  }

}
