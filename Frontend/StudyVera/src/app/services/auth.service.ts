import { Injectable, signal, PLATFORM_ID, Inject, inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment.development';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);

  private apiUrl = `${environment.apiUrl}/auth`;

  isLoggedIn = signal<boolean>(false);

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {
    if (isPlatformBrowser(this.platformId)) {
      const token = localStorage.getItem('accessToken');
      if (token && !this.isTokenExpired()) {
        this.isLoggedIn.set(true);
      } else if (token) {
        this.logout();
      }
    }
  } refreshTokenCall() {
    const accessToken = localStorage.getItem('accessToken');
    const refreshToken = localStorage.getItem('refreshToken');

    // Genellikle .NET backend'leri refresh için hem eski access token'ı hem de refresh token'ı ister
    const payload = {
      accessToken: accessToken,
      refreshToken: refreshToken
    };

    return this.http.post<any>(`${this.apiUrl}/refresh-token`, payload).pipe(
      tap(response => {
        const newToken = response.accessToken;
        const newRefreshToken = response.refreshToken;

        if (newToken && isPlatformBrowser(this.platformId)) {
          localStorage.setItem('accessToken', newToken);

          if (newRefreshToken) {
            localStorage.setItem('refreshToken', newRefreshToken);
          }

          this.isLoggedIn.set(true);
        }
      })
    );
  }

  loginRequest(credentials: any) {
    return this.http.post<any>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => {
        console.log("Normal Login API Cevabı:", response);

        const token = response.accessToken;
        const refreshToken = response.refreshToken;

        if (token && isPlatformBrowser(this.platformId)) {
          localStorage.setItem('accessToken', token);

          if (refreshToken) {
            localStorage.setItem('refreshToken', refreshToken);
          }

          this.isLoggedIn.set(true);
          this.router.navigate(['/profile']);
        } else {
          console.error("DİKKAT: API'den token gelmedi veya değişken adı farklı!", response);
        }
      })
    );
  }

  googleLoginRequest(credential: string) {
    return this.http.post<any>(`${this.apiUrl}/google`, { credential }).pipe(
      tap(response => {
        console.log("Google Login API Cevabı:", response);

        const token = response.accessToken;
        const refreshToken = response.refreshToken;

        if (token && isPlatformBrowser(this.platformId)) {
          localStorage.setItem('accessToken', token);

          if (refreshToken) {
            localStorage.setItem('refreshToken', refreshToken);
          }

          this.isLoggedIn.set(true);
          this.router.navigate(['/profile']);
        } else {
          console.error("DİKKAT: Google API'den token gelmedi!", response);
        }
      })
    );
  }
  private decodeToken(token: string): any {
    try {
      const payloadBase64 = token.split('.')[1];
      return JSON.parse(atob(payloadBase64));
    } catch (e) {
      return null;
    }
  }

  getTokenRemainingMinutes(): number {
    if (!isPlatformBrowser(this.platformId)) return 0;

    const token = localStorage.getItem('accessToken');
    if (!token) return 0;

    const decoded = this.decodeToken(token);
    if (!decoded || !decoded.exp) return 0;

    const expirationDate = decoded.exp * 1000;
    const now = Date.now();

    const diffMs = expirationDate - now;

    if (diffMs <= 0) return 0;

    return Math.floor(diffMs / (1000 * 60));
  }

  isTokenExpired(): boolean {
    const remainingMinutes = this.getTokenRemainingMinutes();
    return remainingMinutes <= 0;
  }
  registerRequest(userData: any) {
    return this.http.post<any>(`${this.apiUrl}/register`, userData);
  }

  logout() {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');

      this.isLoggedIn.set(false);
      this.router.navigate(['/auth/login']);
    }
  }
}