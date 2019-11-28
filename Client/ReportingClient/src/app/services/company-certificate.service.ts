import { CompanyCertificate } from './../models/company-certificate';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyCertificateService {
  private readonly companyCertificateEndpoint = 'api/companyCertificates';

  constructor(
    private auth: AuthService,
    private http: HttpClient
  ) { }

  create(companyCertificate: CompanyCertificate): Observable<any> {
    return this.http.post(this.companyCertificateEndpoint, companyCertificate, { headers: this.auth.authHeader });
  }

  getById(id: string): Observable<any> {
    return this.http.get(this.companyCertificateEndpoint + '/' + id, { headers: this.auth.authHeader });
  }

  getByCompany(): Observable<any> {
    return this.http.get(this.companyCertificateEndpoint + '?companyId=' + this.auth.user.companyId, { headers: this.auth.authHeader });
  }

  getBySupplier(supplierId: string): Observable<any> {
    return this.http.get(this.companyCertificateEndpoint + '?companyId=' + supplierId, { headers: this.auth.authHeader });
  }

  update(companyCertificate: CompanyCertificate): Observable<any> {
    return this.http.put(
      this.companyCertificateEndpoint + '/' + companyCertificate.id,
      companyCertificate, { headers: this.auth.authHeader }
      );
  }

  delete(companyCertificateId: string): Observable<any> {
    return this.http.delete(
      this.companyCertificateEndpoint + '/' + companyCertificateId,
      { headers: this.auth.authHeader }
      );
  }
}
