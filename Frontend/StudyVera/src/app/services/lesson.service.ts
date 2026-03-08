import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { LessonDto } from '../models/lesson.model';

@Injectable({
  providedIn: 'root'
})
export class LessonService {
  private http = inject(HttpClient);
  
  private apiUrl = `${environment.apiUrl}/lessons`;

  getAll(): Observable<LessonDto[]> {
    return this.http.get<LessonDto[]>(this.apiUrl).pipe(
      catchError((error) => {
        console.error('Dersler sunucudan çekilirken bir hata oluştu:', error);        
        return of([]); 
      })
    );
  }
}