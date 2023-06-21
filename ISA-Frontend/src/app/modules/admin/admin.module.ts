import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { AdminHomepageComponent } from './admin-homepage/admin-homepage.component'
import { AdminToolbarComponent } from './admin-toolbar/admin-toolbar.component'
import { MaterialModule } from 'app/material/material.module'
import { FormsModule } from '@angular/forms'
import { ReactiveFormsModule } from '@angular/forms'
import { RouterModule, Routes } from '@angular/router'
import { RoleGuardService } from 'app/auth/role-guard.service'
import { UserListComponent } from './user-list/user-list.component'
import { StaffRegistrationComponent } from './staff-registration/staff-registration.component'
import { PagesModule } from '../pages/pages.module'
import { AdminProfileComponent } from './admin-profile/admin-profile.component'
import { AdminRegistrationComponent } from './admin-registration/admin-registration.component'
import { RegisterCenterComponent } from './register-center/register-center.component'

const routes: Routes = [

  { path: 'admin-home', component:AdminHomepageComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' }},
  { path: 'user-list', component: UserListComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' } },
  {path: 'register-center', component:RegisterCenterComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' }},
  { path: 'register-staff', component: StaffRegistrationComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' } },
  { path: 'register-admin', component: AdminRegistrationComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' } },
  { path: 'admin-profile', component: AdminProfileComponent,
  canActivate: [RoleGuardService], data: { expectedRole: 'ADMIN' } },

];
@NgModule({
  declarations: [
    AdminHomepageComponent,
    AdminToolbarComponent,
    UserListComponent,
    StaffRegistrationComponent,
    AdminProfileComponent,
    AdminRegistrationComponent,
    RegisterCenterComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    PagesModule
  ],
  exports: [
    AdminToolbarComponent
  ]
})
export class AdminModule { }
