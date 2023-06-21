import { Component, OnInit } from '@angular/core';
import { Router,ActivatedRoute, Params } from '@angular/router';
import { Donor } from '../../../model/donor.model';
import { HttpClient } from '@angular/common/http';
import { DonorService } from '../../../services/donor.service';
import { AuthService } from 'app/services/auth.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-edit-donor-profile',
  templateUrl: './edit-donor-profile.component.html',
  styleUrls: ['./edit-donor-profile.component.css']
})
export class EditDonorProfileComponent {
  
  public donor = new Donor();

  public address='';
  public city='';
  public country='';

  public id=0;
  public passChange=false;
  public passConfirm='';
  public passwd='';
  

  constructor(private donorService: DonorService, private router: Router,private authService:AuthService,private toast:NgToastService) { }

  ngOnInit(): void {
   this.id=Number(this.authService.getIdByRole());
    this.donorService.getDonor(this.id).subscribe(res => {
      this.donor = res;
      var split=this.donor.addressString.split(',');
      this.address=split[0];
      this.city=split[1];
      this.country=split[2];
    });
   
  }
  edit(): void {
    if(this.checkValidity()){
      if(this.checkPassword()){
        this.donor.addressString=this.address+','+this.city+','+this.country;
        this.donorService.updateDonor(this.donor).subscribe(res => {
          this.router.navigate(['donor/profile']);
        });
      } else this.toast.error({detail:'Passwords don\'t match!',summary:"Please try again.",duration:5000});
  } else this.toast.error({detail:'Required fields are empty!',summary:"Please complete the form.",duration:5000});

  }

  checkValidity():Boolean{
    
    if (this.donor.name===undefined || this.donor.name==='') return false;
    if (this.donor.surname===undefined || this.donor.surname==='') return false;
    if (this.donor.workplace===undefined || this.donor.workplace==='') return false;
    if (this.donor.phoneNumber===undefined || this.donor.phoneNumber==='') return false;
    if (this.address===undefined || this.address==='' || this.city===undefined || this.city==='' || this.country===undefined || this.country==='') return false;

    return true;
  }

  checkPassword():Boolean{
    if(this.passChange){
      if(this.passwd===undefined || this.passwd==='') return false;
      if (this.passwd!=this.passConfirm) return false;

      this.donor.password=this.passwd;
    }
    return true;

  }
}
