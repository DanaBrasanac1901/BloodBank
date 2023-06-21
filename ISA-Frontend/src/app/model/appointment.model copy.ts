import { SafeUrl } from "@angular/platform-browser";

export class AppointmentViewDTO {
  id: number = 0;
  staffId: number = 0;
  centerName 
  centerId: number = 0;
  startDate: string = '';
  duration: number = 0;
  status: string = '';
  qrCode:string='';
  url: any;
  staffName: string = '';
  staffSurname: string = '';
  
  public constructor(obj?: any) {
    if (obj) {

      this.id = obj.id;
      this.staffId = obj.staffId;
      this.centerId = obj.centerId;
      this.donorId = obj.donorId;
      this.duration = obj.duration;
      this.startDate = obj.startDate;
      this.status = obj.status;
      this.qrCode = obj.qrCode;
      this.staffName = obj.staffName;
      this.staffSurname = obj.staffSurname;
    }


  }
}
