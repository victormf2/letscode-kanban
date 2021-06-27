import { api } from '@/helpers/api-helpers';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Login, LoginResult } from './login';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(
    private readonly httpClient: HttpClient
  ) { }

  login(login: Login): Observable<LoginResult> {
    return this.httpClient.post<LoginResult>(api('login'), login).pipe(
      tap(result => {
        localStorage.setItem('auth', result.jwt)
      })
    )
  }

  get isAuthenticated(): boolean {
    return this.jwt != null
  }

  get jwt(): string | null {
    return localStorage.getItem('auth')
  }
}
