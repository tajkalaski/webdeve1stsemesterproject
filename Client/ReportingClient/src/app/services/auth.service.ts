import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, empty } from 'rxjs';
import { map, switchMap, catchError } from 'rxjs/operators';
import { TokenInfo } from '../models/token-info';
import { Router } from '@angular/router';
import { ApplicationUser } from '../models/application-user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly authEndpoint = 'api/auth';
  private readonly accountEndpoint = 'api/accounts';
  private authStatusSource = new BehaviorSubject<boolean>(false);
  authStatus$ = this.authStatusSource.asObservable();
  user: ApplicationUser;

  constructor(
    private http: HttpClient,
    private router: Router,
    private translateService: TranslateService
  ) { }

  login(email, password) {
    return this.http.post(this.authEndpoint + '/login', { email, password })
      .pipe(
        map((res: JSON) => {
          localStorage.setItem('token', JSON.stringify(res));
          return true;
        }));
  }

  logout() {
    localStorage.removeItem('token');
    this.authStatusSource.next(false);
    this.router.navigate(['/login']);
  }

  refreshToken(tokenInfo: any): Observable<any> {
    return this.http.post(this.authEndpoint + '/refreshToken', { token: tokenInfo })
      .pipe(
        map((res: any) => {
          localStorage.removeItem('token');
          localStorage.setItem('token', JSON.stringify(res));
          this.authStatusSource.next(true);
        }),
        switchMap(() => {
          return this.user$;
        }),
        catchError(() => {
          this.logout();
          // tslint:disable-next-line: deprecation
          return empty();
        })
      );
  }

  getUserAndRefreshJWT(): Observable<ApplicationUser> {
    if (this.checkLocalStorage()) {
      const tokenInfo: TokenInfo = JSON.parse(localStorage.getItem('token'));

      return this.refreshToken(tokenInfo.token);
    }
    return of(null);
  }

  private checkLocalStorage() {
    return !!JSON.parse(localStorage.getItem('token'));
  }

  get authHeader(): {Authorization: any} {
    if (this.checkLocalStorage()) {
      const headerObject = {Authorization: 'bearer ' + JSON.parse(localStorage.getItem('token')).token};
      return headerObject;
    }
  }

  get user$(): Observable<ApplicationUser> {
    if (this.checkLocalStorage()) {
      return this.http.get(this.accountEndpoint, { headers: this.authHeader })
        .pipe(
          switchMap((user: ApplicationUser) => {
            this.translateService.use(user.language);
            this.authStatusSource.next(true);
            this.user = user;
            return of(user);
          }),
          catchError(error => {
            this.logout();
            return of(null);
          })
        );
    }
    return of(null);
  }
}

