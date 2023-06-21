
export class Location {
    Id:number=0;
    Latitude:number=0;
    Longitude:number=0;
    
  
    public constructor(obj?: any) {
      if (obj) {
        this.Id = obj.id;
        this.Latitude=obj.latitude;
        this.Longitude=obj.longitude;
      }
    }
  }