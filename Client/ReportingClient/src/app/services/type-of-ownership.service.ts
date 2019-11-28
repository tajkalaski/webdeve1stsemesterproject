import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TypeOfOwnershipService {
  private readonly typeOfOwnershipEndpoint = "/api/typesofownership";

  constructor(
    private auth: AuthService,
    private http: HttpClient
  ) { }

  getAll(): Observable<any> {
    return this.http.get(this.typeOfOwnershipEndpoint, {"headers": this.auth.authHeader});
  }
}
