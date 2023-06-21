import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { StaffRegistrationDTO } from 'app/model/staffRegistrationDTO';
import { AdminRegistrationDTO } from 'app/model/adminRegistrationDTO';
import { DonorRegistrationDTO } from 'app/model/donorRegistrationDTO';
import { LoginDTO } from 'app/model/loginDTO';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
    apiHost: string = 'http://localhost:16177/api/Authentication';
    headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

    constructor(private http: HttpClient) {
    }
      
    login(user:LoginDTO ): Observable<any> {
        return this.http.post<any>(this.apiHost + '/Login', user, { headers: this.headers });
    }
   
    registerStaff(staff: StaffRegistrationDTO): Observable<any> {
        return this.http.post<any>(this.apiHost + '/Register/Staff', staff, { headers: this.headers });
    }

    registerDonor(donor: DonorRegistrationDTO): Observable<any> {
      return this.http.post<any>(this.apiHost + '/Register/Donor', donor, { headers: this.headers });
  }

    registerAdmin(admin: AdminRegistrationDTO): Observable<any> {
      return this.http.post<any>(this.apiHost + '/Register/Admin', admin, { headers: this.headers });
    }

    changePass(email:string,newPass:string):Observable<any>{
      return this.http.put<any>(this.apiHost + '/ChangePassword?email=' + email+'&newPass='+newPass, { headers: this.headers });
    }

  public setSession(token:any) {
    //localStorage.setItem('currentUser', JSON.stringify(token));
    localStorage.setItem('role', token.claims[5].value);
    localStorage.setItem('userId', token.claims[0].value);
    localStorage.setItem('idByRole', token.claims[1].value);
    localStorage.setItem('fullName', token.claims[3].value + ' ' + token.claims[4].value);
    localStorage.setItem("expires_at", token.validTo);
  }

    logout() {
      localStorage.clear();
    }

    public isLoggedIn() {
      var currentDateTime = new Date().toISOString();
      return currentDateTime < this.getExpiration()!;
    }

    isLoggedOut() {
        return !this.isLoggedIn();
    }

    getExpiration() {
      return localStorage.getItem("expires_at");
    }

    getRole() {
    return localStorage.getItem("role");
    }

    getIdByRole() {
      return localStorage.getItem("idByRole");
    }

    getUserId() {
      return localStorage.getItem("userId");
    }

    getName() {
      return localStorage.getItem("fullName");
    }
  }

