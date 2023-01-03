import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, from, lastValueFrom } from 'rxjs';
import { AccountService } from '../services/account.service';


@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor {

  constructor(private _authService: AccountService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    return from(
      this.prepareRequest(req, next)
    );
  }

  public async prepareRequest(req: HttpRequest<any>, next: HttpHandler) {
    const token = await this._authService.getAccessToken();

    const headers = req.headers.set('Authorization', `Bearer ${token}`);
    const authRequest = req.clone({ headers });
    
    return await lastValueFrom(next.handle(authRequest));
  }
}
