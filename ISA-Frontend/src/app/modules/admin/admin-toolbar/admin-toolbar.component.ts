import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'admin-toolbar',
  templateUrl: './admin-toolbar.component.html',
  styleUrls: ['./admin-toolbar.component.css']
})
export class AdminToolbarComponent implements OnInit {

  constructor(private router:Router,private authService:AuthService) { }

  ngOnInit(): void {
  }

  AdminHomeClick(){
    this.router.navigate(['/admin-home']);
  }

  UsersClick(){
    this.router.navigate(['/user-list']);
  }

  AddCenterClick(){
    this.router.navigate(['/register-center']);
  }

  AddStaffClick(){
    this.router.navigate(['/register-staff']);
  }

  AddAdminClick(){
    this.router.navigate(['/register-admin']);
  }

  LogOutClick(){
    this.authService.logout();
    this.router.navigate(['/']);
  }

  Profile(){
    this.router.navigate(['/admin-profile']); 
  }

}
