import { Component } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { ActivatedRoute, Router } from "@angular/router";
import { Appointment } from "../../../model/appointment.model";
import { BloodCenter } from "../../../model/blood-center.model";
import { Staff } from "../../../model/staff.model";
import { AppointmentService } from "../../../services/appointment.service";
import { AuthService } from "../../../services/auth.service";
import { BloodCenterService } from "../../../services/blood-center.service";
import { StaffService } from "../../../services/staff.service";
import { AppointmentDialogComponent } from "../staff-appointment/appointment-dialog.component";
import { MatLegacyDialog as MatDialog, MatLegacyDialogConfig as MatDialogConfig } from '@angular/material/legacy-dialog';
import { NgToastService } from "ng-angular-popup";

@Component({
  selector: 'app-staff-scheduled',
  templateUrl: './staff-scheduled.component.html',
  styleUrls: ['./staff-scheduled.component.css']
})

export class StaffScheduledComponent {

  public center: BloodCenter | undefined;
  public staff: Staff | undefined;
  public allStaff: Staff[] = [];
  public scheduledAppts: Appointment[] = [];
  public dataSource = new MatTableDataSource<Appointment>();
  public displayedColumns = ['date', 'duration', 'staff', 'status'];
  public selectedAppt:Appointment;

  constructor(private toast: NgToastService, private authService: AuthService, private bloodCenterService: BloodCenterService, private staffService: StaffService, private appointmentService: AppointmentService, private dialog: MatDialog, private route: ActivatedRoute,private router: Router) { }

  ngOnInit(): void {
    var id=Number(this.authService.getIdByRole());
    this.appointmentService.getScheduledForStaff(id).subscribe(res => {
      this.scheduledAppts=res;
      this.dataSource.data=this.scheduledAppts;
    });
  }


  

  selectAppointment(appt:any){
    this.selectedAppt=appt;
  }

  Complete(){
    if(this.selectedAppt===null) this.toast.error({detail:"Appointment not selected!",duration:3000});
    this.appointmentService.completeAppointment(this.selectedAppt).subscribe(res=>{
      this.scheduledAppts=res;
      this.dataSource.data=this.scheduledAppts;
      this.toast.success({detail:"Succesfully completed!",duration:3000});
    })
  }
}
