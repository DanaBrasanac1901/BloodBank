export class Staff {
  id: number = 0;
  email: string = '';
  jmbg:  string= '';
  centerId: number = 0;
  name: string = '';
  surname: string = ''; 
  addressString : string='';
  phoneNumber: number = 0;
  gender: number = 0;
  address: any = null;
  isNew:boolean=false;

  public constructor(obj?: any) {
    if (obj) {
      this.id = obj.id;
      this.email = obj.email;
      this.jmbg = obj.jmbg;
      this.centerId = obj.centerId;
      this.name = obj.name;
      this.surname = obj.surname;
      this.addressString = obj.addressString;
      this.phoneNumber = obj.phoneNumber;
      this.gender = obj.gender;
      this.address = obj.address;
      this.isNew=obj.isNew;
    }
  }
}
