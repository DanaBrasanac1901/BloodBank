import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DeliveryService {

  apiHost: string = 'https://localhost:44371/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  startDelivery(): Observable<any> {
    return this.http.get<any>(this.apiHost + 'Location', { headers: this.headers });
  }

}