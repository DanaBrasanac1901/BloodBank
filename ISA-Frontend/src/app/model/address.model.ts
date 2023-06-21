export class Address {
  id : number = 0;
  city: string = '';
  country: string = '';
  streetAddress: string = '';
  centerId : number = 0; 
  
  public constructor(obj?: any) {
    if (obj) {
      this.id = obj.id;
      this.city = obj.city;
      this.country = obj.country;
      this.streetAddress = obj.streetAddress;
      this.centerId = obj.centerId;
    }
  }
}
