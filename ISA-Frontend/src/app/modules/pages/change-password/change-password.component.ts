import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Staff } from 'app/model/staff.model';
import { AuthService } from 'app/services/auth.service';
import { UserService } from 'app/services/user.service';
import { StaffService } from 'app/services/staff.service';
import { NgToastService } from 'ng-angular-popup';
@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent {

  public oldPassword:string='';
  public newPassword:string='';
  public newPasswordConfirm:string='';
  private staffId:number=0;
  private staff:Staff=new Staff;
  public showToolbar:boolean=true;
  private email:string='';

  constructor(private authService:AuthService,private staffService:StaffService,private toast:NgToastService,private router:Router,private userService:UserService  ){}

  ngOnInit(){
    


    
  }

  changePassword(){
  ///  if(!this.checkValidity()) return;

    var uId=Number(this.authService.getUserId());
    console.log(uId);

    this.userService.getUser(uId).subscribe(res=>{
      this.email=res.email;
    })
/*
    this.authService.authenticate(this.email,this.oldPassword).subscribe(res=>{
      this.authService.changePass(this.email,this.newPassword).subscribe(res=>{
        this.toast.success({detail:'Password changed!',duration:3000});
          this.authService.logout();
          this.router.navigate(['login']);
      },error=>{
        this.toast.error({detail:'Password not updated!',summary:"Something went wrong.",duration:3000});
      })
    },error=>{
      this.toast.error({detail:'Incorrect password!',summary:"Something went wrong.",duration:3000});
    })
    */
  }

  checkValidity(){
    if (this.staff.email===null) {
      this.toast.error({detail:'Something is wrong with your account!',summary:"Please log in again.",duration:3000});
      return false;
    }
    if (this.oldPassword==='' || this.newPassword===undefined || this.newPasswordConfirm===undefined){
      this.toast.error({detail:'Fields empty!',summary:"Please fill every field.",duration:3000});
      return false;
    }
    if(this.newPassword!=this.newPasswordConfirm){
      this.toast.error({detail:'Passwords don\'t match!',summary:"Please try again.",duration:3000});
      return false;
    }

    if(this.newPassword === this.oldPassword){
      this.toast.error({detail:'New password cannot be old password!',summary:"Please try again.",duration:3000});
      return false;
    }


  return true;

  }
}
