import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Donor } from 'app/model/donor.model';
import { DonorService } from '../../../services/donor.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
@Component({
  selector: 'app-donor-profile',
  templateUrl: './donor-profile.component.html',
  styleUrls: ['./donor-profile.component.css']
})
export class DonorProfileComponent {
  
  public donor: Donor=new Donor();
  private id:number=0;

  constructor(private donorService: DonorService, private route: ActivatedRoute, private router: Router,private authService:AuthService) { }

  ngOnInit(): void {

    this.id=Number(this.authService.getIdByRole());
    this.donorService.getDonor(this.id).subscribe(res => {
      this.donor = res;
    });
 
  }
  goToEditPage() {
    this.router.navigate(['donor/edit-profile'])
  }

  changePassword() {
    this.router.navigate(['change-password']);
  }
}
