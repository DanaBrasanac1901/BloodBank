import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { BloodCenter } from '../../../model/blood-center.model';
import { BloodCenterService } from '../../../services/blood-center.service';
import { Router } from '@angular/router';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { Staff } from '../../../model/staff.model';
import { StaffService } from '../../../services/staff.service';
import { AuthService } from '../../../services/auth.service';
import { Address } from 'app/model/address.model';

@Component({
  selector: 'app-blood-center-profile',
  templateUrl: './blood-center-profile.component.html',
  styleUrls: ['./blood-center-profile.component.css']
})
export class BloodCenterProfileComponent {

  public center: BloodCenter | undefined;
  public address: Address | undefined;
  public staff: Staff | undefined;
  public allStaff: Staff[] = [];
  public dataSourceStaff = new MatTableDataSource<Staff>();
  public displayedColumnsStaff = ['name', 'surname', 'email'];


  constructor(private authService: AuthService, private bloodCenterService: BloodCenterService, private staffService: StaffService, private router: Router) { }

  ngOnInit(): void {
//dobijamo ulogovanog staff id preko local storage
    this.staffService.getStaff(Number(this.authService.getIdByRole())).subscribe(res => {
      //dobijamo staff preko id
      this.staff = res;
      //dobijamo centar preko naseg staffa
      this.bloodCenterService.getCenter(res.centerId).subscribe(res1 => {
          this.center = res1;
      });
      this.bloodCenterService.getAddressForCenter(res.centerId).subscribe(res1=>{
          this.address = res1;
      });
      //prikazujemo sve staffove osim ulogovanog iz naseg centra
      this.staffService.getStaffByCenter(res.centerId).subscribe(res1 => {
            this.allStaff = res1.filter(s => s.id != res.id);
            this.dataSourceStaff.data = this.allStaff;
      });
         
    });
  }

  editBloodCenter() {
    this.router.navigate(['staff/edit-center']);
  }

  
}
