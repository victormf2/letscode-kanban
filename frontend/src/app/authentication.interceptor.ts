import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginService } from './login/login.service';
import { api } from '@/helpers/api-helpers';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {

  constructor(
    readonly loginService: LoginService,
    readonly router: Router,
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const authenticatedRequest = this.getAuthenticatedRequest(request)
    return next.handle(authenticatedRequest).pipe(
      tap({
        error: (error) => {
          if (error instanceof HttpErrorResponse && error.status === 401) {
            this.router.navigate(['/login'])
          }
        }
      })
    );
  }

  private getAuthenticatedRequest(request: HttpRequest<unknown>) {
    if (request.url === api('login') && request.method === 'POST') {
      return request
    }
    const authenticatedRequest = request.clone({
      setHeaders: {
        'Authorization': `Bearer ${this.loginService.jwt}`
      }
    })
    return authenticatedRequest
  }
}
