import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { UserQuestionStatDto, AddUserQuestionStatDto } from '../models/user-question-stat.model';

@Injectable({
  providedIn: 'root'
})
export class UserQuestionStatsService {
  private http = inject(HttpClient);
  
  private baseUrl = `${environment.apiUrl}/question-stats`;

  getAll(): Observable<UserQuestionStatDto[]> {
    return this.http.get<UserQuestionStatDto[]>(this.baseUrl).pipe(
      catchError(() => of([]))
    );
  }

  add(request: AddUserQuestionStatDto): Observable<any> {
    return this.http.post(this.baseUrl, request);
  }

  addRange(requests: AddUserQuestionStatDto[]): Observable<any> {
    return this.http.post(`${this.baseUrl}/range`, { items: requests });
  }

  deleteDetail(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/detail/${id}`);
  }

  updateDetail(id: number, request: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/detail/${id}`, request);
  }

  getWeaks(): Observable<UserQuestionStatDto[]> {
    return this.http.get<UserQuestionStatDto[]>(`${this.baseUrl}/weaks`).pipe(
      catchError(() => of([]))
    );
  }
}