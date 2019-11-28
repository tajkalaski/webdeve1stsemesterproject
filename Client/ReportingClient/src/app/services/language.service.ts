import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  private readonly languageEndpoint = "api/languages";
  constructor(
    private auth: AuthService,
    private http: HttpClient
  ) { }

  getAll(): Observable<any> {
    return this.http.get(this.languageEndpoint, { headers: this.auth.authHeader })
  }
}
