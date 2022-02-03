import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
    providedIn: 'root'
})
export class AccountHelper {

    constructor(private jwtHelper: JwtHelperService,) { }

    isUserAuthenticated(): boolean {
        const token = localStorage.getItem("jwt");
        if (token && !this.jwtHelper.isTokenExpired(token)) {
            return true;
        }
        return false;
    }
}