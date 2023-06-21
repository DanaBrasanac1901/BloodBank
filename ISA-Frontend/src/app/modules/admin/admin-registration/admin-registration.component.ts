import { Component } from '@angular/core'
import { AdminRegistrationDTO } from 'app/model/adminRegistrationDTO'
import { AdminService } from 'app/services/admin.service'
import { AuthService } from 'app/services/auth.service'

@Component({
  selector: 'app-admin-registration',
  templateUrl: './admin-registration.component.html',
  styleUrls: ['./admin-registration.component.css']
})
export class AdminRegistrationComponent {

  admin = new AdminRegistrationDTO()

  public constructor(private adminService : AdminService, private authService : AuthService){}
  
  registerAdmin(){
    if(this.checkValidity()) this.authService.registerAdmin(this.admin).subscribe(res =>{
      console.log(res)
    })
  }

  checkValidity(){
    if(this.fieldsAreEmpty(this.admin)) return false
    return true;
  }

  fieldsAreEmpty(object: Object) { 
    return Object.values(object).some(
        value => {
        if (value === null || value === '')  return true
        return false
      })
  }
}
