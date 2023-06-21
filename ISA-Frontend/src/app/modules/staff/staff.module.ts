import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule,Routes } from '@angular/router';
import { RoleGuardService } from 'app/auth/role-guard.service';
import { MaterialModule } from 'app/material/material.module';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDialogModule } from '@angular/material/dialog';
import { PagesModule } from '../pages/pages.module';
import { StaffHomepageComponent } from './staff-homepage/staff-homepage/staff-homepage.component';
import { StaffProfileComponent } from './staff-profile/staff-profile.component';
import { BloodCenterProfileComponent } from './blood-center-profile/blood-center-profile.component';
import { EditStaffProfileComponent } from './edit-staff-profile/edit-staff-profile.component';
import { BloodCenterEditComponent } from './blood-center-edit/blood-center-edit.component';
import { StaffToolbarComponent } from './staff-toolbar/staff-toolbar.component';
import { AppointmentDialogComponent } from './staff-appointment/appointment-dialog.component';
import {MatListModule} from '@angular/material/list';

import { BloodCenterCalendarComponent } from './blood-center-calendar/blood-center-calendar.component';
import { DeliveryMapComponent } from './delivery-map/delivery-map.component';
import { StaffScheduledComponent } from './staff-scheduled/staff-scheduled.component';



const routes: Routes = [

  { path: 'staff/homepage', component:StaffHomepageComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' }},
  { path: 'staff/profile', component: StaffProfileComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' } },
  { path: 'staff/center', component: BloodCenterProfileComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' } },
  { path: 'staff/edit-profile', component: EditStaffProfileComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' } },
  { path: 'staff/edit-center', component: BloodCenterEditComponent,
    canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' }
  },
  {
    path: 'staff/calendar', component: BloodCenterCalendarComponent,
    canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' }
  },
  { path: 'staff/delivery-map', component: DeliveryMapComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' } },
  { path: 'staff/scheduled', component: StaffScheduledComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'STAFF' } },
 
 
];

@NgModule({
  declarations: [
    StaffHomepageComponent,
    StaffToolbarComponent,
    StaffProfileComponent,
    BloodCenterEditComponent,
    BloodCenterProfileComponent,
    BloodCenterCalendarComponent,
    EditStaffProfileComponent,
    AppointmentDialogComponent,
    DeliveryMapComponent,
    StaffScheduledComponent
  ],
  imports: [
    CommonModule,
    PagesModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatListModule,
    RouterModule.forChild(routes),
  ],
  entryComponents: [AppointmentDialogComponent]
  
})
export class StaffModule { }
