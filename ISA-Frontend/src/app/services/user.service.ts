import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../model/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getAllDonors(): Observable<User[]> {
    return this.http.get<User[]>(this.apiHost + 'api/User/Donors', { headers: this.headers });
  }
  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiHost + 'api/User', { headers: this.headers });
  }

  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.apiHost + 'api/User/' + id, { headers: this.headers });
  }

  deleteUser(id: any): Observable<any> {
    return this.http.delete<any>(this.apiHost + 'api/User/' + id, { headers: this.headers });
  }

  createUser(user: any): Observable<any> {
    return this.http.post<any>(this.apiHost + 'api/User', user, { headers: this.headers });
  }

  updateUser(user: any): Observable<any> {
    return this.http.put<any>(this.apiHost + 'api/User/' + user.id, user, { headers: this.headers });
  }

}
