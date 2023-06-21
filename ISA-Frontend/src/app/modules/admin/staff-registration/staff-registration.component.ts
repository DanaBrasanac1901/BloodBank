import { Component, OnInit } from '@angular/core';
import { BloodCenter } from '../../../model/blood-center.model';
import { UserService } from '../../../services/user.service';
import { BloodCenterService } from '../../../services/blood-center.service';
import { AuthService } from 'app/services/auth.service';
import { NgToastService } from 'ng-angular-popup';
import { StaffRegistrationDTO } from 'app/model/staffRegistrationDTO';


@Component({
  selector: 'staff-registration',
  templateUrl: './staff-registration.component.html',
  styleUrls: ['./staff-registration.component.css']
})
export class StaffRegistrationComponent implements OnInit {

  public staff = new StaffRegistrationDTO()
  public centers : BloodCenter[] = []
  public selectedCenter: BloodCenter = new BloodCenter()
  constructor(private bloodService:BloodCenterService, private authService:AuthService,private toast:NgToastService) { }

  ngOnInit(): void {
    this.bloodService.getCenters().subscribe(res => {
      this.centers = res    
    })
  }

  registerStaff()  {
    if (this.checkValidity()) this.authService.registerStaff(this.staff).subscribe(res => {
      console.log(res)
    })
  }

  setCenter(event:any){
    this.staff.centerId=this.selectedCenter.id
  }

  checkValidity(){
    if(this.fieldsAreEmpty(this.staff)) return false
    return true
  }

  fieldsAreEmpty(object: Object) { 
    return Object.values(object).some(
        value => {
        if (value === null || value === '')  return true
        return false
      })
  }

}
