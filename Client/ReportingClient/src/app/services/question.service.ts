import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Observable, empty } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { TokenInfo } from '../models/token-info';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  private readonly questionEndpoint = 'api/questions';


  constructor(
    private http: HttpClient,
    private auth: AuthService
  ) { }

  // TO DO: Create question custom type
  create(question: any): Observable<any> {
    return this.http.post(this.questionEndpoint, question, { headers: this.auth.authHeader});
  }

  get(id: string): Observable<any> {
    return this.http.get(this.questionEndpoint + '/' + id, { headers: this.auth.authHeader});
  }

  getAll(): Observable<any> {
    return this.http.get(this.questionEndpoint, { headers: this.auth.authHeader});
  }
}
