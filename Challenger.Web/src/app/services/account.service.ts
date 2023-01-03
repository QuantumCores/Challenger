import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterModel } from '../components/register/RegisterModel';
import { Observable, Subject } from 'rxjs';
import { LoginModel } from '../components/login/LoginModel';
import { environment } from 'src/environments/environment';
import { Log, User, UserManager, UserManagerSettings } from 'oidc-client';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private apiUrl = `${environment.httpDomain}/Account/api/v1`;
  //private apiUrl = `${environment.httpGateway}/connect/token`;
  //private apiUrl = `${environment.httpDomain}/Account/IdentityTest`;

  private _userManager: UserManager;
  private _user: User | null;
  private _loginChangedSubject = new Subject<boolean>();
  public loginChanged = this._loginChangedSubject.asObservable();

  constructor(private http: HttpClient) {

    this._userManager = new UserManager(this.idpSettings);
  }

  public register(registerModel: RegisterModel): Observable<any> {
    const url = `${this.apiUrl}/register`;
    return this.http.post<RegisterModel>(url, registerModel, httpOptions);
  }

  public login(): Promise<void> {
    return this._userManager.signinRedirect();
  }

  public async isAuthenticated(): Promise<boolean> {
    const user = await this._userManager.getUser();
    if (this._user !== user) {
      this._loginChangedSubject.next(this.checkUser(user));
    }
    this._user = user;
    return this.checkUser(user);
  }

  public async finishLogin(): Promise<User> {
    const user = await this._userManager.signinRedirectCallback();
    this._user = user;
    this._loginChangedSubject.next(this.checkUser(user));
    return user;
  }

  public logout() {
    this._userManager.signoutRedirect();
  }

  public finishLogout() {
    this._user = null;
    return this._userManager.signoutRedirectCallback();
  }

  private checkUser(user: User | null): boolean {
    return !!user && !user.expired;
  }

  public getAccessToken(): Promise<string | null> {
    return this._userManager.getUser()
      .then(user => {
         return !!user && !user.expired ? user.access_token : null;
    })
  }

  // stad biere adres token servera => https://localhost:5001/connect/token
  // public login(registerModel: LoginModel): Observable<any> {    
  //   const url = `${environment.httpDomain}/Account/IdentityTest`;
  //   return this.http.get<any>(url);
  // }

  // public login(registerModel: LoginModel): Observable<any> {
  //   const url = `${environment.httpGateway}/connect/token`;
  //   const data = {
  //     Address: url,
  //     ClientId: "client",
  //     ClientSecret: "secret",
  //     Scope: "api1"
  //   }
  //   return this.http.put<any>(url, data, httpOptions);
  // }

  // to oryginalne dobre
  // public login(registerModel: LoginModel): Observable<any> {
  //   const url = `${this.apiUrl}/login`;
  //   return this.http.post<LoginModel>(url, registerModel, httpOptions);
  // }

  private get idpSettings(): UserManagerSettings {
    return {
      authority: environment.idpAuthority,
      client_id: environment.clientId,
      redirect_uri: `${environment.clientRoot}/signin-callback`,
      scope: "openid profile challenger",
      response_type: "code",
      post_logout_redirect_uri: `${environment.clientRoot}/signout-callback`
    }
  }
}
