import { Address } from "./address.model"

export class BloodCenterRegistrationDTO {
    name = ''
    description = ''
    avgScore  = 0
    openingTime = ''
    closingTime  = ''
    address : Address = new Address() 
    
    
  
    public constructor(obj?: any) {
      if (obj) {
        this.name = obj.name
        this.description = obj.description
        this.avgScore = obj.avgScore
        this.openingTime = obj.openingTime
        this.closingTime = obj.closingTime
        this.address = obj.address
      }
    }
  }