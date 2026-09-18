import { Component, inject, signal, OnInit, OnDestroy, PLATFORM_ID, Inject } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

declare var google: any; // Google objesini tanıtıyoruz

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './login.html'
})
export class Login implements OnInit, OnDestroy {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  private googleClientId = '826303476918-e7ugnlbf84ujrqbiqcet99e0ivgr4ufv.apps.googleusercontent.com';

  isLoading = signal(false);
  errorMessage = signal<string | null>(null);
  googleErrorMessage = signal<string | null>(null);

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  constructor(@Inject(PLATFORM_ID) private platformId: Object) { }

  ngOnInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.loadGoogleScript();
    }
  }

  // Normal Email/Şifre Girişi
  onSubmit() {
    if (this.loginForm.invalid) return;

    this.isLoading.set(true);
    this.errorMessage.set(null);

    this.authService.loginRequest(this.loginForm.value).subscribe({
      next: () => this.isLoading.set(false),
      error: () => {
        this.isLoading.set(false);
        this.errorMessage.set('E-posta adresi veya şifre hatalı.');
      }
    });
  }

  // --- Google Login Mantığı ---
  private loadGoogleScript() {
    // Script zaten yüklüyse tekrar yükleme
    if (document.getElementById('google-jssdk')) {
      this.initGoogleAuth();
      return;
    }

    const script = document.createElement('script');
    script.id = 'google-jssdk';
    script.src = 'https://accounts.google.com/gsi/client';
    script.async = true;
    script.defer = true;
    script.onload = () => this.initGoogleAuth();
    document.head.appendChild(script);
  }

  private initGoogleAuth() {
    if (typeof google === 'undefined' || !this.googleClientId) {
      this.googleErrorMessage.set('Google kimlik doğrulama servisi yüklenemedi.');
      return;
    }

    google.accounts.id.initialize({
      client_id: this.googleClientId,
      callback: this.handleGoogleCredentialResponse.bind(this)
    });

    google.accounts.id.renderButton(
      document.getElementById('googleSignInDiv'),
      { theme: 'outline', size: 'large', width: '100%' } // Buton stili
    );
  }

  private handleGoogleCredentialResponse(response: any) {
    if (!response.credential) return;

    this.googleErrorMessage.set(null);
    this.isLoading.set(true);

    this.authService.googleLoginRequest(response.credential).subscribe({
      next: () => this.isLoading.set(false),
      error: () => {
        this.isLoading.set(false);
        this.googleErrorMessage.set('Google ile giriş başarısız oldu. Lütfen tekrar deneyin.');
      }
    });
  }

  ngOnDestroy() {
    // Bileşen yok olurken gereksiz dinlemeleri temizle
    if (isPlatformBrowser(this.platformId) && typeof google !== 'undefined') {
      google.accounts.id.cancel();
    }
  }
}