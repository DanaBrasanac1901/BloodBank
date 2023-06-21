import { Component } from '@angular/core';
import { AdminService } from 'app/services/admin.service';
import { AuthService } from 'app/services/auth.service';
import { Admin } from 'app/model/admin.model';
import { Router } from '@angular/router';
@Component({
  selector: 'app-admin-profile',
  templateUrl: './admin-profile.component.html',
  styleUrls: ['./admin-profile.component.css']
})
export class AdminProfileComponent {

  public admin:Admin=new Admin();
  constructor(private adminService:AdminService,private authService:AuthService,private router:Router){}

  ngOnInit(){
    var id=Number(this.authService.getIdByRole());
    this.adminService.getAdmin(id).subscribe(res=>{
      this.admin=res;
    })
  }

  changePassword() {
    this.router.navigate(['change-password']);
  }
}
