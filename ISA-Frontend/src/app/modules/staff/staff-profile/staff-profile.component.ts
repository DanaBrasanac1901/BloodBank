import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params} from '@angular/router';
import { UserService } from '../../../services/user.service';
import { Router } from '@angular/router';
import { Staff } from '../../../model/staff.model';
import { StaffService } from '../../../services/staff.service';
import { AuthService } from 'app/services/auth.service';

@Component({
  selector: 'app-staff-profile',
  templateUrl: './staff-profile.component.html',
  styleUrls: ['./staff-profile.component.css']
})
export class StaffProfileComponent {

  public staff: Staff | undefined;

  constructor(private staffService: StaffService, private route: ActivatedRoute, private router: Router,private authService:AuthService) { }

  ngOnInit(): void {
    var id=Number(this.authService.getIdByRole());
    this.staffService.getStaff(id).subscribe(res => {
      this.staff = res;
    });
  }
  editStaffProfile() {
    this.router.navigate(['staff/edit-profile']);
  }
  changePass() {
    this.router.navigate(['change-password']);
  }
}


