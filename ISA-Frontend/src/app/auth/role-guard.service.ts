import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'app/services/auth.service';

@Injectable()
export class RoleGuardService implements CanActivate {


  constructor(public auth: AuthService, public router: Router) { }


  canActivate(route: ActivatedRouteSnapshot): boolean {

     //const expectedRole = route.data['expectedRole'];
   // const tokenRole = this.auth.getRole();
    
    ///if (tokenRole !== expectedRole) {
    //  this.auth.logout();
    //  this.router.navigate(['/']);
    //       return false;
    //  }
     return true;
    }
}
