import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Question } from '../model/question.model';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getQuestions(): Observable<Question[]> {
    return this.http.get<Question[]>(this.apiHost + 'api/Question', { headers: this.headers });
  }

  getQuestion(id: number): Observable<Question> {
    return this.http.get<Question>(this.apiHost + 'api/Question/' + id, { headers: this.headers });
  }

}
