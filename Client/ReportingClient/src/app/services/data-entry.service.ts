import { DataEntry } from './../models/data-entry';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataEntryService {
  private readonly dataEntryEndpoint = 'api/dataEntries';

  constructor(
    private auth: AuthService,
    private http: HttpClient
  ) { }

  create(dataEntry: DataEntry): Observable<any> {
    return this.http.post(this.dataEntryEndpoint, dataEntry, { headers: this.auth.authHeader });
  }

  getAll(): Observable<any> {
    return this.http.get(this.dataEntryEndpoint, { headers: this.auth.authHeader });
  }

  getById(id: string): Observable<any> {
    return this.http.get(this.dataEntryEndpoint + '/' + id, { headers: this.auth.authHeader });
  }

  delete(dataEntryId: string): Observable<any> {
    return this.http.delete(
      this.dataEntryEndpoint + '/' + dataEntryId,
      { headers: this.auth.authHeader }
      );
  }

  update(id: string, dataentry: DataEntry) {
    return this.http.put(this.dataEntryEndpoint + '/' + id, dataentry, { headers: this.auth.authHeader });
  }
}
