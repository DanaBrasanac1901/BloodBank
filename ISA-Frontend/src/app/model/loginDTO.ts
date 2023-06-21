export class LoginDTO {
    email: string = '';
    password:string='';
    
    public constructor(obj?: LoginDTO) {
      if (obj) {
        this.email = obj.email;
        this.password = obj.password;
      }
    }
  }
  