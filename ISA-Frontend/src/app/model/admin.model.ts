export class Admin {
    id: number = 0;
    email: string = '';
    name:string='';
    surname:string='';


  
    public constructor(obj?: any) {
      if (obj) {
        this.email = obj.email;
        this.id = obj.id;
        this.name = obj.name;
        this.surname=obj.surname;
      }
    }
  }
