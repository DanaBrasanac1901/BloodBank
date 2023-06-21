import { Component } from '@angular/core';
import { Address } from 'app/model/address.model';
import { BloodCenter } from 'app/model/blood-center.model';
import { BloodCenterRegistrationDTO } from 'app/model/bloodCenterRegistrationDTO';
import { BloodCenterService } from 'app/services/blood-center.service';

@Component({
  selector: 'app-register-center',
  templateUrl: './register-center.component.html',
  styleUrls: ['./register-center.component.css']
})
export class RegisterCenterComponent {
  public bloodCenter = new BloodCenterRegistrationDTO()
  public address = new Address()
  public openingTime = ''
  public closingTime = ''
  constructor(private bloodCenterService: BloodCenterService) { }

  ngOnInit(): void {
   
  }

  registerCenter() {
    this.bloodCenter.avgScore = 0.0
    this.bloodCenter.address = this.address
    this.bloodCenterService.createCenter(this.bloodCenter).subscribe(res => {
      console.log("created center!")
     })
   
  }
}
