import { Component, OnInit } from '@angular/core';
import { Form } from 'app/model/form.model';
import { Question } from 'app/model/question.model';
import { QuestionService } from 'app/services/question.service';
import { FormService } from 'app/services/form.service';
import { AuthService } from 'app/services/auth.service';

@Component({
  selector: 'app-donor-form',
  templateUrl: './donor-form.component.html',
  styleUrls: ['./donor-form.component.css']
})
export class DonorFormComponent implements OnInit {
  public questions: Question[]=[];
  public form=new Form();
  public donorId:number=0;

  constructor(private questionService:QuestionService,private formService:FormService,private authService:AuthService) { }

  ngOnInit(): void {
    this.donorId=Number(this.authService.getIdByRole());
    this.questionService.getQuestions().subscribe(res => {
      this.questions = res;
    });
  }

  sendForm(){
    
    this.form.donorId=this.donorId;
    this.questions.forEach(element => {
      this.form.questionIds.push(element.id);
      if(element.checked===undefined) this.form.answers.push(false);
      else this.form.answers.push(element.checked);
    });

    console.log(this.form.questionIds,this.form.answers);

    
   this.formService.getForm(this.form.donorId).subscribe(res=>{
      this.form.id=res.id;
      this.formService.updateForm(this.form).subscribe(res => {
        console.log('updated form!');
      });
    },
    error=>{
      this.formService.createForm(this.form).subscribe(res => {
        console.log('created form!');
      });
    });
     
  }


}
