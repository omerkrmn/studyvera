// src/app/services/user-weekly-goal.service.ts

import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { UserWeeklyGoalDto } from '../models/user-weekly-goal.model';

@Injectable({
  providedIn: 'root'
})
export class UserWeeklyGoalService {
  private http = inject(HttpClient);
  
  private baseUrl = `${environment.apiUrl}/weekly-goal`;

  getUserWeeklyGoal(): Observable<UserWeeklyGoalDto> {
    return this.http.get<UserWeeklyGoalDto>(`${this.baseUrl}/summary`).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 0) {
          return throwError(() => new Error('İnternet bağlantınızı kontrol edin, sunucuya ulaşılamıyor.'));
        }
        
        return throwError(() => error);
      })
    );
  }
}