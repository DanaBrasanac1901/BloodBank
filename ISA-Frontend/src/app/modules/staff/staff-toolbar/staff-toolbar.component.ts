import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'staff-toolbar',
  templateUrl: './staff-toolbar.component.html',
  styleUrls: ['./staff-toolbar.component.css']
})
export class StaffToolbarComponent implements OnInit {

  constructor(private router: Router, private authService:AuthService) { }

  ngOnInit(): void {
  }

  StaffHomeClick(){
    this.router.navigate(['/staff/homepage']);
  }

  RegClick(){

    this.router.navigate(['/staff/register']);
  }

  ProfileClick(){

    this.router.navigate(['/staff/profile']);
  }

  DonorsClick() {
   
  }

  CalendarClick() {

    this.router.navigate(['/staff/calendar']);
  }

  BloodClick() {
    this.router.navigate(['/staff/delivery-map']);
  }

  CenterProfileClick(){

    this.router.navigate(['/staff/center']);
  }
  Scheduled(){

    this.router.navigate(['/staff/scheduled']);
  }

 

  LogOutClick(){
    this.authService.logout();
    this.router.navigate(['/']);
  }

}
