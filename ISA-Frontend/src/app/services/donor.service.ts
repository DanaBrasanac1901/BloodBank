import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Donor } from 'app/model/donor.model';
@Injectable({
  providedIn: 'root'
})
export class DonorService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getDonors(): Observable<Donor[]> {
    return this.http.get<Donor[]>(this.apiHost + 'api/Donor', { headers: this.headers });
  }

  getDonor(id: number): Observable<Donor> {
    return this.http.get<Donor>(this.apiHost + 'api/Donor/' + id, { headers: this.headers });
  }

  updateDonor(donor: any): Observable<any> {
    return this.http.put<any>(this.apiHost + 'api/Donor/' + donor.id, donor, { headers: this.headers });
  }

}
