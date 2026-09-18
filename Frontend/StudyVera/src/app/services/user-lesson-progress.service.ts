// services/user-lesson-progress.service.ts
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  UserLessonProgressDto,
  AddUserLessonProgressDto,
  UpdateUserLessonProgressDto
} from '../models/user-lesson-progress.model';

export interface AddRangeLessonProgressDto {
  lessonProgresses: AddUserLessonProgressDto[];
}

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

  addRange(dto: AddRangeLessonProgressDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/add-range`, dto);
  }

  update(ulpId: number, dto: UpdateUserLessonProgressDto): Observable<any> {
    return this.http.put(`${this.apiUrl}/${ulpId}`, dto);
  }

  reviewTopic(topicId: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/review-topic/${topicId}`, null);
  }
}