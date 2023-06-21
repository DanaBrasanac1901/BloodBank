import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Admin } from 'app/model/admin.model';
import { AdminRegistrationDTO } from 'app/model/adminRegistrationDTO';
import { StaffRegistrationDTO } from 'app/model/staffRegistrationDTO';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  apiHost: string = 'http://localhost:16177/api/Admin';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' })

  constructor(private http: HttpClient) { }

  getAdmins(): Observable<Admin[]> {
    return this.http.get<Admin[]>(this.apiHost, { headers: this.headers })
  }

  getAdmin(id: number): Observable<Admin> {
    return this.http.get<Admin>(this.apiHost + '/' + id, { headers: this.headers })
  }

  registerStaff(dto : StaffRegistrationDTO) : Observable<any> {
    return this.http.post(this.apiHost + '/Register/Staff', dto, {headers : this.headers})
  }

  registerAdmin(dto : AdminRegistrationDTO) : Observable<any> {
    return this.http.post(this.apiHost + '/Register/Admin', dto, {headers : this.headers})
  }

  

}
