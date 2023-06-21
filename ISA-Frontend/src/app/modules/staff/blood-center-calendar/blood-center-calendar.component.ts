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
  selector: 'app-blood-center-calendar',
  templateUrl: './blood-center-calendar.component.html',
  styleUrls: ['./blood-center-calendar.component.css']
})

export class BloodCenterCalendarComponent {

  public center: BloodCenter | undefined;
  public staff: Staff | undefined;
  public allStaff: Staff[] = [];
  public futureAppointments: Appointment[] = [];
  public dataSource = new MatTableDataSource<Appointment>();
  public displayedColumns = ['date', 'duration', 'staff', 'status'];

  constructor(private toast: NgToastService, private authService: AuthService, private bloodCenterService: BloodCenterService, private staffService: StaffService, private appointmentService: AppointmentService, private dialog: MatDialog, private route: ActivatedRoute,private router: Router) { }

  ngOnInit(): void {
    //dobijamo ulogovanog staff id preko local storage
    this.staffService.getStaff(Number(this.authService.getIdByRole())).subscribe(res => {
      //dobijamo staff preko id
      this.staff = res;
      //dobijamo centar preko naseg staffa
      this.bloodCenterService.getCenter(res.centerId).subscribe(res1 => {
        this.center = res1;
        this.loadApps();
      });
    
    
    });
  }

  loadApps() {
    this.appointmentService.getFutureApptsByCenter(this.center!.id).subscribe(res1 => {
      this.futureAppointments = res1;
      this.dataSource.data = this.futureAppointments;
    });


  }
  addAppointment() {
    const startTime = new Date(this.center!.workTimeStart);
    const endTime = new Date(this.center!.workTimeEnd);
    const dialogRef = this.dialog.open(AppointmentDialogComponent, { height: '600px', width: '400px', data: { start: startTime.getHours(), end: endTime.getHours() } });
    dialogRef.afterClosed().subscribe(
      data => {
        let appointment = new Appointment();
        appointment.staffId = data.staff;
        appointment.centerId = this.center!.id;
        appointment.donorId = 0;
        appointment.startDate = data.dateTime.format('YYYY-MM-DD HH:mm:ss');
        appointment.duration = data.duration;
        appointment.status = "AVAILABLE";

        this.appointmentService.scheduleStaff(appointment).subscribe(
          response => {
            this.toast.success({ detail: "Appointment scheduled!", summary: '', duration: 3000 });
            this.loadApps();
          },
          error => {
            this.toast.error({ detail: 'Something went wrong!', summary: "", duration: 3000 });
          }
        )
      }

    );

  }
}
