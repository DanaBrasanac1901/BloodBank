export class Donor {
  id: number = 0;
  email: string = '';
  password: string = '';
  name: string = '';
  surname:string='';
  addressString: string = '';
  address: any = '';
  phoneNumber: string = '';
  gender: string = '';
  jmbg: string = '';
  profession: string = '';
  workplace: string = '';
  strikes:number=0;
  

  public constructor(obj?: any) {
    if (obj) {
      this.email = obj.email;
      this.password = obj.password;
      this.id = obj.id;
      this.name = obj.name;
      this.surname=obj.surname;
      this.addressString = obj.addressString;
      this.phoneNumber = obj.phoneNumber;
      this.gender = obj.gender;
      this.jmbg = obj.jmbg;
      this.workplace = obj.workplace;
      this.profession = obj.profession;
      this.address = obj.address;
      this.strikes=obj.strikes;
    }
  }
}
