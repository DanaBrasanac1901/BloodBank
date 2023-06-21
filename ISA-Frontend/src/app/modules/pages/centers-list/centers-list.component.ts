import { Component, OnInit, ViewChild } from '@angular/core';
import { BloodCenter } from '../../../model/blood-center.model';
import { BloodCenterService } from '../../../services/blood-center.service';
import { MatTableDataSource } from '@angular/material/table';
import {MatSort, Sort} from '@angular/material/sort';


@Component({
  selector: 'centers-list',
  templateUrl: './centers-list.component.html',
  styleUrls: ['./centers-list.component.css']
})
export class CentersListComponent implements OnInit {

  @ViewChild('empTbSort') empTbSort = new MatSort();
  public centers: BloodCenter[] = [];
  public searchText: any= "";
  public filterScore: any= "";
  public filterCity: any="";
  public open: any="";
  public dataSource = new MatTableDataSource<BloodCenter>();
  public cities: string[]=[];
  public displayedColumns = ['name','address','description','avgScore','openHours'];
  

  constructor(private centerService:BloodCenterService) { }

  ngOnInit(): void {
    this.loadCenters();
    this.loadCities(); 
    this.centers.sort((b,a) => (
    a.avgScore - b.avgScore   
    ));

  }

  loadCities()
  {
      this.centerService.getCities().subscribe(res=>{
        this.cities=res;
      })
  }

  loadCenters() {
    this.centerService.getCenters().subscribe(res => {
      this.centers = res;
      this.dataSource.data = this.centers;
      this.dataSource.sort = this.empTbSort;
    });
  }



  applySearch() {
    if (this.searchText === '') this.loadCenters();
    this.centerService.getSearchResults(this.searchText).subscribe(res => {
      this.centers = res;
      this.dataSource.data = this.centers;
    });
  }


 filterByScore(event: Event) {
    this.dataSource.filterPredicate = function (centers,filter) {
    return centers.avgScore > parseFloat(filter);
}
   const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
 }

  filterOpen(event: Event) {

    this.dataSource.filterPredicate = function (centers,filter) {
     if((event.target as HTMLInputElement).value == 'open')
        return true;
        else return false;
      }
    let dateTime = new Date();
    const filterValue = dateTime.getHours.toString();
   this.dataSource.filter = filterValue.trim().toLowerCase();
  }


  filterByCity(event: Event) {
    this.dataSource.filterPredicate = function (centers,filter) {
    return true;
   }
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
