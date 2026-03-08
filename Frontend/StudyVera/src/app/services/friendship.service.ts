// src/app/services/friendship.service.ts

import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { FriendDto, PendingUserDto } from '../models/friendship.model';

@Injectable({ providedIn: 'root' })
export class FriendshipService {
    private http = inject(HttpClient);
    private baseUrl = `${environment.apiUrl}/friendship`;

    addFriend(friendUserName: string): Observable<any> {
        return this.http.post(`${this.baseUrl}/request/${friendUserName}`, null);
    }

    acceptFriend(friendUserName: string): Observable<any> {
        return this.http.post(`${this.baseUrl}/accept/${friendUserName}`, null);
    }

    getAllFriends(): Observable<FriendDto[]> {
        return this.http.get<FriendDto[]>(this.baseUrl).pipe(
            catchError(() => of([]))
        );
    }

    getReceivedRequests(): Observable<PendingUserDto[]> {
        return this.http.get<PendingUserDto[]>(`${this.baseUrl}/requests`).pipe(
            catchError(() => of([]))
        );
    }
}