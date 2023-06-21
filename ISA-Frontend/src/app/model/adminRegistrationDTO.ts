export class AdminRegistrationDTO {
    email = ''
    password = ''
    userType = 'ADMIN'
    name = ''
    surname = ''
    gender = ''
  
  
    public constructor(obj?: AdminRegistrationDTO) {
      if (obj) {
        this.email = obj.email
        this.password = obj.password
        this.name = obj.name
        this.surname = obj.surname
        this.gender = obj.gender
      }
    }
  }
  