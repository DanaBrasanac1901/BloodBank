import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router, Params } from "@angular/router";
import { AuthService } from "app/services/auth.service";
import { Staff } from "../../../model/staff.model";
import { StaffService } from "../../../services/staff.service";

@Component({
  selector: 'app-edit-staff-profile',
  templateUrl: './edit-staff-profile.component.html',
  styleUrls: ['./edit-staff-profile.component.css']
})

export class EditStaffProfileComponent implements OnInit {

  public staff: Staff | undefined;

  constructor(private authService: AuthService, private staffService: StaffService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    
    this.staffService.getStaff(Number(this.authService.getIdByRole())).subscribe(res => {
      this.staff = res;
    });
   
  }

  public updateStaff(): void {
    if (!this.isValidInput()) return;
    console.log(this.staff);
    this.staffService.updateStaff(this.staff).subscribe(res => {
      this.router.navigate(['staff/profile']);
    });
  }

  private isValidInput(): boolean {
    return  this.staff?.name != '' && this.staff?.surname != '' && this.staff?.addressString!='' && this.staff?.phoneNumber!=0;
  }
}
