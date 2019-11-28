import { AuthService } from './auth.service';
import { ApplicationUser } from './../models/application-user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly userEndpoint = 'api/users';

  constructor(
    private http: HttpClient,
    private auth: AuthService
  ) {
  }

  getByEmail(email: string): Observable<any> {
    return this.http.get(this.userEndpoint + '/' + email, { headers: this.auth.authHeader });
  }

  getByCompany(): Observable<any> {
    return this.http.get(this.userEndpoint + '?companyId=' + this.auth.user.companyId, {headers: this.auth.authHeader });
  }

  create(user: ApplicationUser): Observable<any> {
    return this.http.post(this.userEndpoint, user, { headers: this.auth.authHeader });
  }

  update(user: ApplicationUser): Observable<any> {
    return this.http.put(this.userEndpoint, user, { headers: this.auth.authHeader });
  }

  delete(email: string): Observable<any> {
    return this.http.delete(this.userEndpoint + '/' + email, { headers: this.auth.authHeader });
  }
}
