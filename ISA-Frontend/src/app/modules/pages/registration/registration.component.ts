import { Component, OnInit } from '@angular/core'
import { AuthService } from 'app/services/auth.service'
import { Router } from '@angular/router'
import { NgToastService } from 'ng-angular-popup'
import { DonorRegistrationDTO } from 'app/model/donorRegistrationDTO'
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  
  public donor = new DonorRegistrationDTO()
  public passwordConfirm = ''

  constructor( private authService: AuthService,private router: Router, private toast:NgToastService) {
  }

  ngOnInit(): void {
    
  }

  registerDonor()  {
    
    if(this.checkValidity()) this.authService.registerDonor(this.donor).subscribe(res => {
        console.log("res");
        this.toast.success({detail:"Sent activation link!",summary:'Check your email.',duration:5000});
    })

  }


  checkValidity(){
    if (this.fieldsAreEmpty(this.donor)) return false

    if (this.donor.password != this.passwordConfirm){
      this.toast.error({detail:'Passwords don\'t match',summary:"Please retype the password.",duration:5000});
      return false;
    }


    if (isNaN(Number(this.donor.JMBG))){
      this.toast.error({detail:'Jmbg contains only numbers!',summary:"Please enter a valid jmbg.",duration:5000});
      return false;
    }
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
