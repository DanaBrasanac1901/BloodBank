export class Form {
    id:number=0;
    donorId:number=0;
    questionIds: number[] = [];
    answers: boolean[]=[];
  
    public constructor(obj?: any) {
      if (obj) {
        this.id=obj.id;
        this.donorId=obj.donorId;
        this.questionIds=obj.questionIds;
        this.answers=obj.answers;
      }
    }
  }
  