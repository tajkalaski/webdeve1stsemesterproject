import { Certificate } from './../models/certificate';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CertificateService {
  private readonly certificateEndpoint = 'api/certificates';

  constructor(
    private auth: AuthService,
    private http: HttpClient
  ) { }

  create(certificate: Certificate): Observable<any> {
    return this.http.post(this.certificateEndpoint, certificate, { headers: this.auth.authHeader });
  }

  getAll(): Observable<any> {
    return this.http.get(this.certificateEndpoint, { headers: this.auth.authHeader });
  }
}
