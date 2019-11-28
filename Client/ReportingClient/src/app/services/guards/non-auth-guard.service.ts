import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Router, CanActivate } from '@angular/router';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root'
})
export class NonAuthGuard implements CanActivate {

  constructor(
    private auth: AuthService,
    private router: Router
  ) { }

  canActivate() {
    return this.auth.user$
      .pipe(
        map(user => {
          if (!user) return true;

          this.router.navigate(['/']);
          return false;
        }));
  }
}
