import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { ProfileViewModel } from '../models/profile-summary.model';

@Injectable({
  providedIn: 'root'
})
export class ProfileSummaryService {
  private http = inject(HttpClient);
  
  private baseUrl = `${environment.apiUrl}/profile/summary`;

  getProfileSummary(): Observable<ProfileViewModel | null> {
    return this.http.get<ProfileViewModel>(this.baseUrl).pipe(
      catchError((error) => {
        console.error('ProfileSummaryService.getProfileSummary error:', error);        
        return of(null); 
      })
    );
  }
}