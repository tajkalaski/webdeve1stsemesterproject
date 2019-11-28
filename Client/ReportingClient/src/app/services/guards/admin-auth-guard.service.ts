import { ApplicationUser } from '../../models/application-user';
import { Observable } from 'rxjs';
import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from '../auth.service';
import { map } from 'rxjs/operators';

@Injectable()
export class AdminAuthGuard implements CanActivate {

  constructor(
    private auth: AuthService,
    private router: Router
  ) { }

  canActivate(): Observable<boolean> {
    return this.auth.user$
      .pipe(
        map((user: ApplicationUser) => user.role == "Admin" || user.role == "Supplier")
      );
  }
}
