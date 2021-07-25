import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { CuentaService } from '../services/cuenta.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private cuentaService: CuentaService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let  token: string;

    token = JSON.parse(localStorage.getItem('user'))?.token;
    if(token){
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      })
    }
    return next.handle(request);
  }
}