import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { TopicDto } from '../models/topic.model';

@Injectable({
  providedIn: 'root'
})
export class TopicService {
  private http = inject(HttpClient);
  
  private apiUrl = `${environment.apiUrl}/topics`;

  getTopics(searchTerm?: string): Observable<TopicDto[]> {
    let params = new HttpParams();
    
    if (searchTerm && searchTerm.trim() !== '') {
      params = params.set('searchTerm', searchTerm.trim());
    }

    return this.http.get<TopicDto[]>(this.apiUrl, { params }).pipe(
      catchError((error) => {
        console.error(`TopicService.getTopics error:`, error);
        return of([]); 
      })
    );
  }
}