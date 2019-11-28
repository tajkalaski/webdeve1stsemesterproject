import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QuestionSubcategoryService {
  private readonly questionSubCategoryEndpoint = "/api/questionSubCategories"

  constructor(
    private http: HttpClient,
    private auth: AuthService
  ) { }

  getAll(): Observable<any> {
    return this.http.get(this.questionSubCategoryEndpoint, { headers: this.auth.authHeader });
  }
}
