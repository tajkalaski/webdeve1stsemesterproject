import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class LegalFormService {
  private readonly legalFormEndpoint = "/api/legalforms";

  constructor(
    private auth: AuthService,
    private http: HttpClient
  ) { }

  getAll(): Observable<any> {
    return this.http.get(this.legalFormEndpoint, {"headers": this.auth.authHeader});
  }
}
