import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import AssessmentQuestion from '../models/assessment-question';

@Injectable({
  providedIn: 'root'
})
export class AssessmentQuestionService {
  private readonly assessmentQuestionEndpoint = 'api/assessmentQuestions';

  constructor(
    private auth: AuthService,
    private http: HttpClient
  ) { }

  createMany(addedAssessmentQuestions: AssessmentQuestion[]): Observable<any> {
    return this.http.post(this.assessmentQuestionEndpoint + '/createMany', addedAssessmentQuestions, { headers: this.auth.authHeader });
  }

  deleteMany(deletedAssessmentQuestions: AssessmentQuestion[]): Observable<any> {
    return this.http.post(this.assessmentQuestionEndpoint + '/deleteMany', deletedAssessmentQuestions, { headers: this.auth.authHeader });
  }

  getByAssessment(assessmentId: string): Observable<any> {
    return this.http.get(this.assessmentQuestionEndpoint + '?assessmentId=' + assessmentId, { headers: this.auth.authHeader });
  }

  getByUser(email: string): Observable<any> {
    return this.http.get(this.assessmentQuestionEndpoint + '/user/' + email, { headers: this.auth.authHeader });
  }
}
