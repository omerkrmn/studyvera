import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { UserActivityHistoryDto } from '../models/user-activity-history.model';

@Injectable({
  providedIn: 'root',
})
export class UserHistoryService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/user-activities`;

  getAll(): Observable<UserActivityHistoryDto[]> {
    return this.http.get<UserActivityHistoryDto[]>(this.baseUrl).pipe(
      catchError((error) => {
        console.error('Aktivite geçmişi çekilirken hata oluştu:', error);
        return of([]);
      })
    );
  }

  getAllByDate(): Observable<Date[]> {
    return this.http.get<string[]>(`${this.baseUrl}/get-all-by-date`).pipe(
      map(dates =>
        dates
          .map(d => {
            const dateObj = new Date(d);
            dateObj.setHours(0, 0, 0, 0);
            return dateObj;
          })
          .sort((a, b) => a.getTime() - b.getTime())
      ),
      catchError((error) => {
        console.error('Tarih verileri çekilirken hata oluştu:', error);
        return of([]);
      })
    );
  }
}
