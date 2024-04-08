import { Injectable } from "@angular/core";
import { accessTokenModel } from "app/model/accessTokenModel";
import {jwtDecode} from 'jwt-decode';

@Injectable({
    providedIn: 'root',
})
export class JwtService {
    constructor(){

    }

    public setSession(token: accessTokenModel) {
        localStorage.setItem('currentSession', JSON.stringify(token.accessToken));
      }
    
    public getCurrentSession(){
        return localStorage.getItem('currentSession')
    }

    public decodeToken(token : string){
        return jwtDecode(token);
    }

    public isLoggedIn() {
        var currentDateTime = new Date().toISOString();
        return currentDateTime < this.getExpiration()!;
      }
  
      isLoggedOut() {
          return !this.isLoggedIn();
      }
  
      getExpiration() {
        const token = this.getCurrentSession()
       // if(token) this.decodeToken(token)[''];
        return localStorage.getItem("expires_at");
      }
  
      getRole() {
      return localStorage.getItem("role");
      }
  
      getUserId() {
        return localStorage.getItem("userId");
      }
  
      getName() {
        return localStorage.getItem("fullName");
      }
}