import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/services/auth.service';

@Component({
  selector: 'donor-toolbar',
  templateUrl: './donor-toolbar.component.html',
  styleUrls: ['./donor-toolbar.component.css']
})
export class DonorToolbarComponent implements OnInit {

  constructor(private router: Router,private authService: AuthService) { }

  ngOnInit(): void {
  }

  DonorHomeClick(){
    this.router.navigate(['/donor/homepage']);
  }

  
  FormClick(){
    this.router.navigate(['/donor/form']);
  }

  HistoryClick() {
    this.router.navigate(['/donor/history']);
  }

  
  ProfileClick(){
    this.router.navigate(['/donor/profile']);
  }

  ScheduleSuggestionsClick(){
    this.router.navigate(['/donor/schedule-appt']);
  }

  ScheduleRegularClick(){
    this.router.navigate(['/donor/make-appointment']);
  }

  ApptsClick(){
    this.router.navigate(['/donor/appointments']);
  }
  
  LogOutClick(){
    this.authService.logout();
    this.router.navigate(['/']);
  }



}
