import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { RoleGuardService } from 'app/auth/role-guard.service';
import { DonorHomepageComponent } from './donor-homepage/donor-homepage.component';
import { DonorProfileComponent } from './donor-profile/donor-profile.component';
import { EditDonorProfileComponent } from './edit-donor-profile/edit-donor-profile.component';
import { DonorFormComponent } from './donor-form/donor-form.component';
import { DonorAppointmentScheduleComponent } from './donor-appointment-schedule/donor-appointment-schedule.component';
import { DonorAppointmentsComponent } from './donor-appointments/donor-appointments.component';
import { DonorMadeAppointmentComponent } from './donor-made-appointment/donor-made-appointment.component';
import { DonorToolbarComponent } from './donor-toolbar/donor-toolbar.component';
import { MaterialModule } from 'app/material/material.module';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { PagesModule } from '../pages/pages.module';
import { DonorQrsComponent } from './donor-qrs/donor-qrs.component';
import { MatSortModule } from '@angular/material/sort';
import { DonorHistoryComponent } from './donor-history/donor-history.component';

const routes: Routes = [
  
  { path: 'donor/homepage', component: DonorHomepageComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' } },
  { path: 'donor/profile', component: DonorProfileComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' } },
  { path: 'donor/edit-profile', component: EditDonorProfileComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' } },
  {path: 'donor/form', component:DonorFormComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' }},
  { path: 'donor/schedule-appt', component:DonorAppointmentScheduleComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' }},
  { path: 'donor/appointments', component:DonorAppointmentsComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' }},
  { path: 'donor/make-appointment', component: DonorMadeAppointmentComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' } },
  { path: 'donor/qrs', component: DonorQrsComponent,
    canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' }
  },
  {
    path: 'donor/history', component: DonorHistoryComponent,
    canActivate: [RoleGuardService], data: { expectedRole: 'DONOR' } },
];

@NgModule({
  declarations: [
    DonorHomepageComponent,
    DonorToolbarComponent,
    DonorMadeAppointmentComponent,
    DonorProfileComponent,
    EditDonorProfileComponent,
    DonorFormComponent,
    DonorAppointmentScheduleComponent,
    DonorAppointmentsComponent,
    DonorMadeAppointmentComponent,
    DonorQrsComponent,
    DonorHistoryComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    PagesModule,
    MatSortModule
  ]
})
export class DonorModule { }
