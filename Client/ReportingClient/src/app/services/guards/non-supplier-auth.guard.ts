import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';
import { ApplicationUser } from 'src/app/models/application-user';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class NonSupplierAuthGuard implements CanActivate {

  constructor(
    private auth: AuthService,
    private router: Router
  ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    return this.auth.user$
      .pipe(
        map((user: ApplicationUser) => {
          if (user.role != "Supplier") return true;

          this.router.navigate(['/manage-company']);
          return false;
        })
      );
  }
}
