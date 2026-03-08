// src/app/services/user-profile.service.ts

import { Injectable, inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { isPlatformBrowser } from '@angular/common';
import { environment } from '../../environments/environment';
import { UserProfileDto } from '../models/user-profile.model';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {
  private http = inject(HttpClient);
  private platformId = inject(PLATFORM_ID);
  
  private apiUrl = `${environment.apiUrl}/profile/user-profile`;
  private cacheKey = 'user_profile_data';

  
  getProfile(): Observable<UserProfileDto | null> {
    if (isPlatformBrowser(this.platformId)) {
      const cachedProfile = localStorage.getItem(this.cacheKey);
      if (cachedProfile) {
        return of(JSON.parse(cachedProfile) as UserProfileDto);
      }
    }

    return this.http.get<UserProfileDto>(this.apiUrl).pipe(
      tap(profile => {
        if (profile && isPlatformBrowser(this.platformId)) {
          localStorage.setItem(this.cacheKey, JSON.stringify(profile));
        }
      }),
      catchError(() => {
        return of(null);
      })
    );
  }


  updateProfile(dto: Partial<UserProfileDto>): Observable<any> {
    return this.http.patch(this.apiUrl, dto).pipe(
      tap(() => {
        this.clearCache();
      })
    );
  }


  clearCache(): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem(this.cacheKey);
    }
  }
}