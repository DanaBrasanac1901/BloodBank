export class StaffRegistrationDTO {
    email = ''
    password = ''
    userType = 'STAFF'
    name = ''
    surname = ''
    gender = ''
    centerId = 0
  
    public constructor(obj?: any) {
      if (obj) {
        this.email = obj.email
        this.password = obj.password
        this.name = obj.name
        this.surname = obj.surname
        this.gender = obj.gender
        this.centerId = obj.centerId
      }
    }
  }
  