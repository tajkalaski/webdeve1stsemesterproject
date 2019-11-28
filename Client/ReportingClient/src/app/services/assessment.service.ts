import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
import { map } from 'rxjs/operators';
import Assessment from '../models/assessment';

@Injectable({
  providedIn: 'root'
})
export class AssessmentService {
  private readonly assessmentEndpoint = 'api/assessments';


  constructor(
    private http: HttpClient,
    private auth: AuthService
  ) { }

  create(assessment: Assessment): Observable<any> {
    return this.http.post(this.assessmentEndpoint, assessment, { headers: this.auth.authHeader });
  }

  update(id: string, assessment: Assessment): Observable<any> {
    return this.http.put(this.assessmentEndpoint + '/' + id, assessment, { headers: this.auth.authHeader });
  }

  delete(id: string) {
    return this.http.delete(this.assessmentEndpoint + '/' + id, { headers: this.auth.authHeader });
  }

  getByCompany(): Observable<any> {
    return this.http.get(this.assessmentEndpoint + '?companyid=' + this.auth.user.companyId, { headers: this.auth.authHeader })
      .pipe(
        map((assessments: any[]) => {
          assessments.sort((n1, n2) => {
            if (n1.endYear < n2.endYear) {
              return 1;
            }
            if (n1.endYear > n2.endYear) {
              return -1;
            }
            return 0;
          });
          return assessments;
        })
      );
  }

  getById(id: string): Observable<any> {
    return this.http.get(this.assessmentEndpoint + '/' + id, { headers: this.auth.authHeader });
  }
}
