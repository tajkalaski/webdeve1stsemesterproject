import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Company } from '../models/company';
import { HttpClient } from '@angular/common/http';
import { ApplicationUser } from '../models/application-user';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {
  private readonly supplierEndpoint = 'api/suppliers';

  constructor(
    private auth: AuthService,
    private http: HttpClient
  ) { }

  create(supplier: any): Observable<any> {
    return this.http.post(this.supplierEndpoint, supplier, { headers: this.auth.authHeader });
  }

  createSupplierRelation(supplierRelation: any): Observable<any> {
    return this.http.post(this.supplierEndpoint + '/relations', supplierRelation, { headers: this.auth.authHeader });
  }

  createSupplierUser(supplierId: string, supplierUser: any): Observable<any> {
    return this.http.post(this.supplierEndpoint + '/' + supplierId + '/users', supplierUser, { headers: this.auth.authHeader });
  }

  getById(id: string): Observable<any> {
    return this.http.get(this.supplierEndpoint + '/' + id, { headers: this.auth.authHeader });
  }

  getByCompanyId(companyId: string) {
    return this.http.get(this.supplierEndpoint + '?companyId=' + companyId, { headers: this.auth.authHeader });
  }
}
