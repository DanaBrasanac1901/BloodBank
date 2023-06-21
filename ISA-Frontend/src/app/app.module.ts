import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { MaterialModule } from "./material/material.module";
import { PagesModule } from "./modules/pages/pages.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import {NgToastModule} from 'ng-angular-popup'
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { AuthInterceptor } from "./auth/auth.interceptor";
import { RoleGuardService } from "./auth/role-guard.service";
import { DatePipe } from "@angular/common"
import {MatToolbarModule} from '@angular/material/toolbar'; 
import {MatTableModule} from '@angular/material/table'
import { AdminModule } from "./modules/admin/admin.module";
import { DonorModule } from "./modules/donor/donor.module";
import { StaffModule } from "./modules/staff/staff.module";
import { MatDialogModule } from "@angular/material/dialog";
import { GoogleMapsModule } from "@angular/google-maps";
import { MapsModule } from '@syncfusion/ej2-angular-maps';
import { LegendService, MarkerService, MapsTooltipService, DataLabelService, BubbleService, NavigationLineService, SelectionService, AnnotationsService, ZoomService } from '@syncfusion/ej2-angular-maps';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    PagesModule,
    FormsModule,
    NgToastModule,
    MatToolbarModule,
    MatTableModule,
    MatDialogModule,
    AdminModule,
    DonorModule,
    StaffModule,
    GoogleMapsModule,
    MapsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    RoleGuardService,
    DatePipe,
    LegendService, 
    MarkerService, 
    MapsTooltipService, 
    DataLabelService, 
    BubbleService, 
    NavigationLineService , 
    SelectionService, 
    AnnotationsService, 
    ZoomService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
