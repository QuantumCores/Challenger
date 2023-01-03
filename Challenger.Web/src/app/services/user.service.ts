import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IfStmt } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserBasicDto } from '../components/measurements/UserBasicDto';
//import { AccountHelper } from '../helpers/AccountHelper';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private userBasic: UserBasicDto;
  private apiUrl = `${environment.httpDomain}/User`;

  constructor(
    private http: HttpClient,
    //private accountHelper: AccountHelper
    ) { }

  public getUserBasic(): Observable<UserBasicDto> {
    let subject = new Subject<UserBasicDto>();

    if (this.userBasic) {
      //TODO
      let userId = ""; //this.accountHelper.getUserId();
      // if (userId && this.userBasic.correlationId == userId) {
      //   return of(this.userBasic);
      // }
    }
    else {
      const url = `${this.apiUrl}/api/v1/basic`;

      this.http.get<UserBasicDto>(url).subscribe(
        (userDto) => {
          this.userBasic = userDto;
          subject.next(userDto);
        }
      );
    }
    return subject;
  }
}
