import { Injectable, inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { isPlatformBrowser } from '@angular/common';
import { environment } from '../../environments/environment';
import { LessonScheduleDto, AddLessonScheduleDto } from '../models/lesson-schedule.model';

@Injectable({ providedIn: 'root' })
export class LessonScheduleService {
  private http = inject(HttpClient);
  private platformId = inject(PLATFORM_ID);
  private apiUrl = `${environment.apiUrl}/lesson-schedule`;
  private cacheKey = 'lessonSchedules';

  getAll(): Observable<LessonScheduleDto[]> {
    if (isPlatformBrowser(this.platformId)) {
      const cached = localStorage.getItem(this.cacheKey);
      if (cached) return of(JSON.parse(cached));
    }

    return this.http.get<LessonScheduleDto[]>(this.apiUrl).pipe(
      tap(data => {
        if (isPlatformBrowser(this.platformId)) {
          localStorage.setItem(this.cacheKey, JSON.stringify(data));
        }
      }),
      catchError(err => {
        console.error('Program alınırken hata oluştu:', err);
        return of([]);
      })
    );
  }

  add(dto: AddLessonScheduleDto): Observable<any> {
    return this.http.post(this.apiUrl, dto).pipe(
      tap(() => {
        if (isPlatformBrowser(this.platformId)) {
          localStorage.removeItem(this.cacheKey);
        }
      })
    );
  }

  clearCache(): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem(this.cacheKey);
    }
  }
}