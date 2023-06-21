import { Component, OnInit, ViewChild } from '@angular/core';
import { StaffService } from 'app/services/staff.service';
import { Staff } from 'app/model/staff.model';
import { AuthService } from 'app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-staff-homepage',
  templateUrl: './staff-homepage.component.html',
  styleUrls: ['./staff-homepage.component.css']
})

export class StaffHomepageComponent implements OnInit {

  public staff: Staff | undefined;
  private staffId:number=0;

  constructor(private staffService:StaffService, private authService:AuthService,private router:Router) { }

  ngOnInit(): void {
    this.staffId=Number(this.authService.getIdByRole());
    this.staffService.getStaff(this.staffId).subscribe(res => {
      this.staff = res;
      console.log(this.staff.isNew);

      if(this.staff.isNew){
        this.router.navigate(['/staff/change-password']);
      }
    });

  }

}

