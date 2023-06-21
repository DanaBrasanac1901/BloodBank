import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Appointment } from '../../../model/appointment.model';
import { AppointmentService } from '../../../services/appointment.service';
import { NgToastService } from 'ng-angular-popup';
import { AuthService } from '../../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-donor-appointments',
  templateUrl: './donor-appointments.component.html',
  styleUrls: ['./donor-appointments.component.css']
})
export class DonorAppointmentsComponent {

  public dataSource = new MatTableDataSource<Appointment>();
  public appointments:Appointment[]=[];
  public displayedColumns = ['staff','date','duration'];

  public selectedAppt:Appointment=new Appointment;
  public donorId:any;

  public apptId:number=0;
  constructor(private apptService:AppointmentService, 
              private toast:NgToastService,
              private authService:AuthService,
              private router:Router) { }

  ngOnInit(): void {
    this.donorId = Number(this.authService.getIdByRole());
    this.apptService.getScheduledForDonor(this.donorId).subscribe(res => {
      this.appointments = res;
      console.log(res);
      this.dataSource.data=this.appointments;
    });
  }

  selectAppointment(appt:any){
    this.selectedAppt=appt;
    console.log(appt);
  }

  cancelAppointment(){
    this.apptService.cancelAppt(this.selectedAppt).subscribe(res => {
      this.appointments=res;
      this.dataSource.data=this.appointments;
      this.toast.success({detail:"Appointment cancelled!",summary:'You can\'t reschedule on the same date.',duration:3000});

    }, error=>{
      this.toast.error({detail:'Something went wrong!',summary:"",duration:3000});
    });
  }

  suggestionSchedule(){
    this.router.navigate(['/donor/schedule-appt']);
  }

  regSchedule(){
    this.router.navigate(['/donor/make-appointment']);
  }

  qrsClick(){
    this.router.navigate(['/donor/qrs']);

  }

}
