import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private readonly roleEndpoint = "api/roles"

  constructor(
    private auth: AuthService,
    private http: HttpClient
  ) { }

  getAll(): Observable<any> {
    return this.http.get(this.roleEndpoint, { "headers": this.auth.authHeader })
  }
}
