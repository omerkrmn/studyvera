import { Component, inject, signal, OnInit, OnDestroy, PLATFORM_ID, Inject } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

declare var google: any;

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './register.html'
})
export class Register implements OnInit, OnDestroy {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  private googleClientId = '826303476918-e7ugnlbf84ujrqbiqcet99e0ivgr4ufv.apps.googleusercontent.com';

  targetExams = [
    { name: 'KPSS', id: 1 },
    { name: 'ALES', id: 2 },
    { name: 'YDS', id: 3 }
  ];

  isLoading = signal(false);
  errorMessages = signal<string[]>([]);
  googleErrorMessage = signal<string | null>(null);

  registerForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    userName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    targetExam: ['', Validators.required]
  });

  constructor(@Inject(PLATFORM_ID) private platformId: Object) { }

  ngOnInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.loadGoogleScript();
    }
  }

  // register.ts içerisindeki onSubmit metodunu şu şekilde güncelle:
  onSubmit() {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);
    this.errorMessages.set([]);

    // HTML'den gelen string değeri (.NET için) integer'a çeviriyoruz
    const formValues = this.registerForm.value;
    const requestPayload = {
      ...formValues,
      targetExam: Number(formValues.targetExam) // ID'yi sayı olarak ayarladık
    };

    this.authService.registerRequest(requestPayload).subscribe({
      next: () => {
        // Otomatik giriş için sadece email ve password lazım
        const loginData = {
          email: formValues.email,
          password: formValues.password
        };

        this.authService.loginRequest(loginData).subscribe({
          next: () => this.isLoading.set(false),
          error: () => {
            this.isLoading.set(false);
            this.router.navigate(['/auth/login']);
          }
        });
      },
      error: (err) => {
        this.isLoading.set(false);
        const errorMsg = err.error?.Message || err.error?.message;
        
        if (errorMsg) {
          this.errorMessages.set(errorMsg.split(',').map((e: string) => e.trim()));
          if (typeof window !== 'undefined' && (window as any).showToast) {
            (window as any).showToast('error', errorMsg);
          }
        } else {
          this.errorMessages.set(['Kayıt sırasında bir hata oluştu.']);
          if (typeof window !== 'undefined' && (window as any).showToast) {
            (window as any).showToast('error', 'Kayıt sırasında bir hata oluştu.');
          }
        }
      }
    });
  }

  private loadGoogleScript() {
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
    if (typeof google === 'undefined' || !this.googleClientId) return;

    google.accounts.id.initialize({
      client_id: this.googleClientId,
      callback: this.handleGoogleCredentialResponse.bind(this)
    });

    google.accounts.id.renderButton(
      document.getElementById('googleSignInDiv'),
      { theme: 'outline', size: 'large', width: '100%' }
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
        this.googleErrorMessage.set('Google ile işlem başarısız oldu.');
      }
    });
  }

  ngOnDestroy() {
    if (isPlatformBrowser(this.platformId) && typeof google !== 'undefined') {
      google.accounts.id.cancel();
    }
  }
}