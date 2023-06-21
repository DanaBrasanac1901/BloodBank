
export class BloodCenter {
  name: string = '';
  id: number = 0;
  stringAddress: string = '';
  description: string = ' ';
  openHours: string = ' ';
  avgScore: number = 0;
  amountA:number=0;
  amountB:number=0;
  amountAB:number=0;
  amountO: number = 0;
  workTimeEnd: string='';
  workTimeStart: string = '';
   
  
  

  public constructor(obj?: any) {
    if (obj) {
      this.id = obj.id;
      this.name = obj.name;
      this.openHours = obj.openHours;
      this.stringAddress = obj.stringAddress;
      this.description = obj.description;
      this.avgScore = obj.avgScore;
      this.amountA=obj.amountA;
      this.amountB=obj.amountB;
      this.amountAB=obj.amountAB;
      this.amountO = obj.amountO;
      this.workTimeEnd = obj.workTimeEnd;
      this.workTimeStart = obj.workTimeStart;
    }
  }
}
