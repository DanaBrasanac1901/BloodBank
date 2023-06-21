export class Question {
    id:number=0;
    text: string = '';
    checked: boolean = false;
  
    public constructor(obj?: any) {
      if (obj) {
        this.id=obj.id;
        this.text=obj.text;
      }
    }
  }
  