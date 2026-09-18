import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { UserProfileDto } from '../models/user-profile.model';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {
  private http = inject(HttpClient);
  
  private apiUrl = `${environment.apiUrl}/profile/user-profile`;

  getProfile(): Observable<UserProfileDto | null> {
    return this.http.get<UserProfileDto>(this.apiUrl).pipe(
      catchError((error) => {
        console.error('UserProfileService.getProfile error:', error);
        return of(null);
      })
    );
  }

  updateProfile(dto: Partial<UserProfileDto>): Observable<any> {
    return this.http.patch(this.apiUrl, dto);
  }
}