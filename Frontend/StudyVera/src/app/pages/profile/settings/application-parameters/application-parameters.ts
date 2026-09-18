import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserProfileService } from '../../../../services/user-profile.service';
import { ThemeService } from '../../../../services/theme.service';
import { UserProfileDto } from '../../../../models/user-profile.model';

@Component({
  selector: 'app-application-parameters',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './application-parameters.html',
  styleUrls: ['./application-parameters.css']
})
export class ApplicationParametersComponent implements OnInit {
  private profileService = inject(UserProfileService);
  private themeService = inject(ThemeService);
  private cdr = inject(ChangeDetectorRef);

  parameters: UserProfileDto = {
    weeklyQuestionGoal: 0,
    dailyStudyMinuteGoal: 0,
    dailyReminderHour: 0,
    isProfilePublic: false,
    showRankInLeaderboard: false,
    allowFriendRequests: false,
    theme: 'Light',
    language: 'tr',
    currentTitle: '',
    createdAt: '',
    updatedAt: ''
  };

  isLoading = true;
  isSaving = false;

  ngOnInit() {
    this.loadProfile();
  }

  loadProfile() {
    this.isLoading = true;
    this.profileService.getProfile().subscribe({
      next: (profile: any) => {
        if (profile) {
          // Handle both wrapped { data: { ... } } and unwrapped { ... } responses
          const profileData = profile.data ? profile.data : profile;
          this.parameters = { ...this.parameters, ...profileData };
          console.log('Loaded profile data:', this.parameters);
        }
        this.isLoading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  saveChanges() {
    this.isSaving = true;
    this.profileService.updateProfile(this.parameters).subscribe({
      next: () => {
        this.isSaving = false;
        
        // Sync theme globally in frontend
        if (this.parameters.theme) {
          const lowerTheme = this.parameters.theme.toLowerCase() as 'light' | 'dark';
          this.themeService.setTheme(lowerTheme);
        }

        // Show toast
        if (typeof window !== 'undefined' && (window as any).showToast) {
          (window as any).showToast('success', 'Profiliniz başarıyla güncellendi!');
        }

        // Fetch from DB again to refresh the data
        this.profileService.getProfile().subscribe();
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.isSaving = false;
        const errMsg = err.error?.Message || err.error?.message || 'Profiliniz güncellenirken bir hata oluştu!';
        if (typeof window !== 'undefined' && (window as any).showToast) {
          (window as any).showToast('error', errMsg);
        }
        this.cdr.detectChanges();
      }
    });
  }
}
