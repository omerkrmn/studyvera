import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { QuestionStatDetailDto } from '../models/question-stat-detail.model'; 

@Injectable({
  providedIn: 'root'
})
export class QuestionStatDetailService {
  private http = inject(HttpClient);
  
  private baseUrl = `${environment.apiUrl}/question-stat-details`;

  getAll(questionStatId: number): Observable<QuestionStatDetailDto[]> {
    return this.http.get<QuestionStatDetailDto[]>(`${this.baseUrl}/${questionStatId}`).pipe(
      catchError((error) => {
        console.error(`QuestionStatDetailService.getAll error:`, error);
        return of([]); 
      })
    );
  }
}