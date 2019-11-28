import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router, CanActivate } from '@angular/router';
import { map, catchError } from 'rxjs/operators';
import { empty } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private auth: AuthService,
    private router: Router
  ) { }

  canActivate() {
    return this.auth.getUserAndRefreshJWT()
      .pipe(
        map(user => {
          if (user) { return true; }
          this.router.navigate(['/login']);
          return false;
        })
      );
  }
}
