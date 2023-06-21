import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'start-toolbar',
  templateUrl: './start-toolbar.component.html',
  styleUrls: ['./start-toolbar.component.css']
})
export class StartToolbarComponent {

  constructor(private router:Router) { }


  HomeClick(){
    this.router.navigate(['/']);
  }

  LoginClick(){
    this.router.navigate(['/login']);
  }
  RegisterClick(){
    this.router.navigate(['/register']);
  }
}
