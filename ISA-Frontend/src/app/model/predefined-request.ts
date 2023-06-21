import { SafeUrl } from "@angular/platform-browser";

export class PredefinedRequestDDTO {
  staffId: number = 0;
  centerId: number = 0;
  startDate: string = '';
  duration: number = 0;
  status: string = '';
  public constructor(obj?: any) {
    if (obj) {

      
      this.staffId = obj.staffId;
      this.centerId = obj.centerId;
      this.duration = obj.duration;
      this.startDate = obj.startDate;
      this.status = obj.status;
    }


  }
}
