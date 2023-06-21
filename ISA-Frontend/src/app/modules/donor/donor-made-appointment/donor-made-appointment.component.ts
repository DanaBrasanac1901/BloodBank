import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { FormControl, FormGroup } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';
import { Router } from '@angular/router';
import * as moment from 'moment';
import { NgToastService } from 'ng-angular-popup';
import { Address } from '../../../model/address.model';
import { Appointment } from '../../../model/appointment.model';
import { BloodCenter } from '../../../model/blood-center.model';
import { Donor } from '../../../model/donor.model';
import { AppointmentService } from '../../../services/appointment.service';
import { AuthService } from '../../../services/auth.service';
import { BloodCenterService } from '../../../services/blood-center.service';
import { DonorService } from '../../../services/donor.service';
import { FormService } from '../../../services/form.service';


@Component({
  selector: 'app-donor-made-appointment',
  templateUrl: './donor-made-appointment.component.html',
  styleUrls: ['./donor-made-appointment.component.css']
})
export class DonorMadeAppointmentComponent implements OnInit {

  public centers: BloodCenter[] = [];
  public dataSource = new MatTableDataSource<BloodCenter>();
  public selectedRow: BloodCenter=new BloodCenter;
  public selectedIndex = 0;
  public displayedColumns = ['name','description', 'avgScore'];
  public minDate = new Date();
  public dateTime = new FormControl(moment(new Date()));
  public chosenDate = '';
  public showForm: boolean = false;
  public donorId: number = 0;
  public donor!: Donor;
  public center!: BloodCenter;

  constructor(private appointmentService: AppointmentService, 
              private donorService:DonorService, 
              private authService: AuthService,
              private formService: FormService,
              private toast:NgToastService,
              private router:Router) { }

  ngOnInit(): void {
    this.donorId = Number(this.authService.getIdByRole());
    this.donorService.getDonor(this.donorId).subscribe(res => {
      this.donor = res;
    })
  }
 
  applyDateTime() {

    if (this.dateTime.value != null) {

     this.chosenDate = this.dateTime.value.format('YYYY-MM-DD HH:mm:ss');
      this.appointmentService.getCentersForDateTime(this.chosenDate).subscribe(res => {
        this.centers = res.sort((a, b) => b.avgScore - a.avgScore);
        this.dataSource.data = this.centers;
      });

    }
   
  }

  chooseCenter() {
   
    this.formService.isEligible(this.donorId).subscribe(response => {    
        this.makeAppointment(); 
    },
      error => {
        this.toast.error({detail:'You haven\'t filled the form or you already gave blood recently!',summary:"",duration:3000});
      });
  
  }

  selectCenter(appt:any){
    this.selectedRow=appt;
  }

  makeAppointment() {
    var appointment = new Appointment();
    appointment.startDate = this.chosenDate;
    console.log(this.chosenDate);
    appointment.centerId = this.selectedRow.id;
    appointment.donorId = this.donorId;
    appointment.status = 'scheduled';
    appointment.duration = 30;
    this.appointmentService.scheduleDonorMade(appointment).subscribe(
      res => {
        this.toast.success({ detail: "Appointment scheduled!", summary: '', duration: 3000 });
      },
      error => {
        this.toast.error({ detail: 'Something went wrong!', summary: "", duration: 3000 });
      });
  }

  backClick(){
    this.router.navigate(['/donor/appointments']);
  }

}
