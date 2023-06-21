export class DonorRegistrationDTO {
    email = ''
    password = ''
    userType = 'DONOR'
    name = ''
    surname = ''
    address = ''
    city = ''
    state = ''
    gender = ''
    JMBG = ''
    workplace = ''
    profession = ''
    phoneNumber = ''
  
    public constructor(obj?: any) {
      if (obj) {
        this.email = obj.email
        this.password = obj.password
        this.name = obj.name
        this.surname = obj.surname
        this.address = obj.address
        this.city = obj.city
        this.state = obj.state
        this.gender = obj.gender
        this.JMBG = obj.JMBG
        this.workplace = obj.workplace
        this.profession = obj.profession
        this.phoneNumber = obj.phoneNumber
      }
    }
  }
  