import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Appointment } from 'app/model/appointment.model';
import { NgToastService } from 'ng-angular-popup';
import { AppointmentService } from 'app/services/appointment.service';
import { AuthService } from 'app/services/auth.service';
import { DomSanitizer } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';

@Component({
  selector: 'app-donor-qrs',
  templateUrl: './donor-qrs.component.html',
  styleUrls: ['./donor-qrs.component.css']
})
export class DonorQrsComponent {

  @ViewChild('empTbSort') empTbSort = new MatSort();
  public dataSource = new MatTableDataSource<Appointment>();
  public appointments:Appointment[]=[];
  public appointmentsCopy:Appointment[]=[];
  public displayedColumns = ['staffId','date'];
  public filterStatus: any= "";
  private donorId:number=0;

  constructor(private apptService:AppointmentService, 
              private toast:NgToastService,
              private authService:AuthService,
              private sanitizer:DomSanitizer,
              private router:Router) { }

  ngOnInit(): void {
    this.donorId=Number(this.authService.getIdByRole());

    this.apptService.getAllForDonor(this.donorId).subscribe(res => {
      this.appointments=res;
      this.appointmentsCopy=res;
      this.dataSource.data=this.appointments;

      this.appointments.forEach(appointment => {
        console.log(appointment.startDate);
        if (appointment.qrCode!=undefined){
        appointment.qrCode='data:image/png;base64,' + appointment.qrCode;
        appointment.url = this.sanitizer.bypassSecurityTrustUrl(appointment.qrCode);
        } else {
          appointment.url='/assets/images/white_x.png';
        }
      });


      
    });

    
   
  }

  backClick(){
    this.router.navigate(['/donor/appointments']);

  }

  filterByStatus(event: Event) {

      const filterValue = (event.target as HTMLInputElement).value;

      if(filterValue==='All'){
        this.dataSource.filter='';
        return;
      }
      this.dataSource.filter = filterValue.trim().toLowerCase();
      this.dataSource.filterPredicate = function (appointments,filter) {
        return appointments.status.toLocaleLowerCase().includes(filter.toLocaleLowerCase());
      }
    
  }

}
