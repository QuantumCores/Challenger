import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
    providedIn: 'root'
})
export class AccountHelper {

    private headerName: string = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
    constructor(private jwtHelper: JwtHelperService,) { }

    isUserAuthenticated(): boolean {
        const token = localStorage.getItem("jwt");
        if (token && !this.jwtHelper.isTokenExpired(token)) {
            return true;
        }
        return false;
    }

    getUserEmail(): string {
        const token = localStorage.getItem("jwt");
        if (token) {
            let decoded = this.jwtHelper.decodeToken(token);
            if (decoded) {
                return decoded[this.headerName];
            }
        }
        return '';
    }
}