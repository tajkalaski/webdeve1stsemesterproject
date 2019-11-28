import { ApplicationUser } from './../models/application-user';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private readonly accountEndpoint = 'api/accounts';

  constructor(
    private http: HttpClient,
    private auth: AuthService
  ) {
  }

  register(user: ApplicationUser): Observable<any> {
    return this.http.post(this.accountEndpoint, user, { headers: this.auth.authHeader });
  }

  update(user: ApplicationUser): Observable<any> {
    return this.http.put(this.accountEndpoint, user, { headers: this.auth.authHeader });
  }

  delete(email: string): Observable<any> {
    return this.http.delete(this.accountEndpoint + '/' + email, { headers: this.auth.authHeader });
  }

  changePassword(loginInfo: any): Observable<any> {
    return this.http.post(this.accountEndpoint + '/changePassword', { headers: this.auth.authHeader });
  }

  resetPassword(email: string): Observable<any> {
    return this.http.post(this.accountEndpoint + '/resetPassword', { headers: this.auth.authHeader });
  }
}
