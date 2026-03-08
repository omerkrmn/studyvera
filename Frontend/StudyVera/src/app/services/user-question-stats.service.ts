
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { UserQuestionStatDto, AddUserQuestionStatDto } from '../models/user-question-stat.model';

@Injectable({
  providedIn: 'root'
})
export class UserQuestionStatsService {
  private http = inject(HttpClient);
  
  private baseUrl = `${environment.apiUrl}/question-stats`;

  getAll(): Observable<UserQuestionStatDto[]> {
    return this.http.get<UserQuestionStatDto[]>(this.baseUrl);
  }

  add(request: AddUserQuestionStatDto): Observable<any> {
    return this.http.post(this.baseUrl, request);
  }
}