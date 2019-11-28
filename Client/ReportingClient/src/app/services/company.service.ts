import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Company } from '../models/company';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private readonly companiesEndpoint = '/api/companies';

  constructor(
    private http: HttpClient,
    private auth: AuthService
  ) { }

  getById(id: string): Observable<any> {
    return this.http.get(this.companiesEndpoint + '/' + id, { headers: this.auth.authHeader });
  }

  update(id: string, company: Company) {
    return this.http.put(this.companiesEndpoint + '/' + id, company, { headers: this.auth.authHeader });
  }
}
