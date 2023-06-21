import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Staff } from '../model/staff.model';

@Injectable({
  providedIn: 'root'
})
export class StaffService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getAll(): Observable<Staff[]> {
    return this.http.get<Staff[]>(this.apiHost + 'api/Staff', { headers: this.headers });
  }

  getStaff(id: number): Observable<Staff> {
    return this.http.get<Staff>(this.apiHost + 'api/Staff/' + id, { headers: this.headers });
  }

  updateStaff(staff: any): Observable<any> {
    return this.http.put<any>(this.apiHost + 'api/Staff/' + staff.id, staff, { headers: this.headers });
  }

  getStaffByCenter(centerId: number): Observable<Staff[]> {
    return this.http.get<Staff[]>(this.apiHost + 'api/Staff/center/' + centerId, { headers: this.headers });
  }
 
}
