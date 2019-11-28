import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class QuestionCategoryService {
  private readonly questionCategoryEndpoint = "/api/questionCategories"

  constructor(
    private http: HttpClient,
    private auth: AuthService
  ) { }

  getAll(): Observable<any> {
    return this.http.get(this.questionCategoryEndpoint, { headers: this.auth.authHeader });
  }
}
