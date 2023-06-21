import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { NgToastService } from 'ng-angular-popup';
import { Router } from '@angular/router';
import { Appointment } from '../../../model/appointment.model';
import { AppointmentService } from '../../../services/appointment.service';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-donor-history',
  templateUrl: './donor-history.component.html',
  styleUrls: ['./donor-history.component.css']
})
export class DonorHistoryComponent {

  public dataSource = new MatTableDataSource<Appointment>();
  public appointments: Appointment[] = [];
  public displayedColumns = ['center','staff', 'date', 'duration','status'];
  public donorId: number = 0;

  constructor(private apptService: AppointmentService, private toast: NgToastService, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.donorId = Number(this.authService.getIdByRole());
    this.apptService.getHistoryForDonor(this.donorId).subscribe(res => {
      console.log(res);
      this.appointments = res;
      this.dataSource.data = this.appointments;
    });
  }



}
