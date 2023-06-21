import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../../model/user.model';
import { UserService } from '../../../services/user.service';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import {MatSort, Sort} from '@angular/material/sort';


@Component({
  selector: 'user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  @ViewChild('empTbSort') empTbSort = new MatSort();
  public users: User[] = [];
  public searchText: any= "";
  public dataSource = new MatTableDataSource<User>();
  public displayedColumns = ['name','surname','email','type'];
  

  constructor(private userService:UserService) { }

  ngOnInit(): void {
    this.userService.getAllUsers().subscribe(res => {
      this.users = res;
      this.dataSource.data = this.users;
      
    });
    const userCopy = [...this.users]; 

  }
  applySearch(event: Event) {
    this.dataSource.filterPredicate = function (user,filter) {
      return user.name.toLocaleLowerCase().startsWith(filter.toLocaleLowerCase());
    }
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
