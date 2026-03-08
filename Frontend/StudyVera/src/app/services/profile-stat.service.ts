import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { ProfileStatDto, ScoreBoardDto } from '../models/profile-stat.model';

@Injectable({
  providedIn: 'root'
})
export class ProfileStatService {
  private http = inject(HttpClient);
  
  private apiUrl = `${environment.apiUrl}/profile/stats`;

  getScore(): Observable<ProfileStatDto | null> {
    return this.http.get<ProfileStatDto>(`${this.apiUrl}/me`).pipe(
      catchError((error) => {
        console.error('ProfileStatService.getScore error:', error);
        return of(null); 
      })
    );
  }

  getScoreBoard(pageNumber: number = 1, pageSize: number = 5): Observable<ScoreBoardDto[]> {
    const params = new HttpParams()
      .set('PageNumber', pageNumber.toString())
      .set('PageSize', pageSize.toString());

    return this.http.get<ScoreBoardDto[]>(this.apiUrl, { params }).pipe(
      catchError((error) => {
        console.error('Veri alınırken hata oluştu! Lütfen daha sonra tekrar deneyiniz.', error);
        return of([]);
      })
    );
  }
}