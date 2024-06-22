import { HttpClient } from '@angular/common/http';
import { Call } from '../models/call.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CallDto } from '../models/callDto.model';

@Injectable({
  providedIn: 'root'
})
export class CallService {

  constructor(private http: HttpClient) { }
  
  private apiUrl = 'https://localhost:7272/api/Calls';

  getCalls(): Observable<Call[]> {
    return this.http.get<Call[]>(this.apiUrl);
  }

  getCall(id: number): Observable<Call> {
    return this.http.get<Call>(`${this.apiUrl}/${id}`);
  }

  createCall(call: Call): Observable<any> {
    return this.http.post<any>(this.apiUrl, call);
  }

  updateCall(id: number, call: Call): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, call);
  }

  deleteCall(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
  
}