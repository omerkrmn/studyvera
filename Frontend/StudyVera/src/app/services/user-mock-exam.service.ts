import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { 
  CreateUserMockExamCommand, 
  UserMockExamDetailResponse, 
  UserMockExamResponse 
} from '../models/user-mock-exam.model';

@Injectable({
  providedIn: 'root'
})
export class UserMockExamService {
  private http = inject(HttpClient);
  
  private baseUrl = `${environment.apiUrl}/mocks`;

  createMockExam(request: CreateUserMockExamCommand): Observable<number> {
    return this.http.post<number>(this.baseUrl, request).pipe(
      catchError((error) => {
        console.error('CreateMockExam Error:', error);
        return of(0);
      })
    );
  }

  getMockExamDetail(id: number): Observable<UserMockExamDetailResponse | null> {
    return this.http.get<UserMockExamDetailResponse>(`${this.baseUrl}/${id}`).pipe(
      catchError(() => {
        return of(null); 
      })
    );
  }

  getUserMockExams(): Observable<UserMockExamResponse[]> {
    return this.http.get<UserMockExamResponse[]>(`${this.baseUrl}/history`).pipe(
      catchError(() => {
        return of([]); 
      })
    );
  }
}