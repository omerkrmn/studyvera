// services/user-lesson-progress.service.ts
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { 
  UserLessonProgressDto, 
  AddUserLessonProgressDto, 
  UpdateUserLessonProgressDto, 
  ProgressStatus 
} from '../models/user-lesson-progress.model';

@Injectable({
  providedIn: 'root'
})
export class UserLessonProgressService {
  private http = inject(HttpClient);
  
  private apiUrl = `${environment.apiUrl}/user-lesson-progresses`;

  getAll(): Observable<UserLessonProgressDto[]> {
    return this.http.get<UserLessonProgressDto[]>(this.apiUrl);
  }

  add(dto: AddUserLessonProgressDto): Observable<any> {
    return this.http.post(this.apiUrl, dto);
  }

  update(ulpId: number): Observable<any> {
    const updateBody: UpdateUserLessonProgressDto = {
      progressStatus: ProgressStatus.Completed
    };

    return this.http.put(`${this.apiUrl}/${ulpId}`, updateBody);
  }

  addRange(): void {
    throw new Error('Method not implemented.');
  }
}