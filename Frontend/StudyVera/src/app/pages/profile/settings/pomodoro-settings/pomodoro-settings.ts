import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PomodoroService } from '../../../../services/pomodoro.service';

@Component({
  selector: 'app-pomodoro-settings',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pomodoro-settings.html',
  styleUrls: ['./pomodoro-settings.css']
})
export class PomodoroSettingsComponent implements OnInit {
  private pomodoroService = inject(PomodoroService);
  private cdr = inject(ChangeDetectorRef);

  settings = {
    personalMessage: '',
    isAudioAlertEnabled: true
  };

  isSaving = false;

  ngOnInit() {
    this.loadSettings();
  }

  loadSettings() {
    // Copy current settings from global service
    this.settings.personalMessage = this.pomodoroService.settings.personalMessage;
    this.settings.isAudioAlertEnabled = this.pomodoroService.settings.isAudioAlertEnabled;
  }

  saveSettings() {
    this.isSaving = true;
    // Simulate short saving delay for premium UX feel
    setTimeout(() => {
      this.pomodoroService.settings.personalMessage = this.settings.personalMessage;
      this.pomodoroService.settings.isAudioAlertEnabled = this.settings.isAudioAlertEnabled;
      this.pomodoroService.saveSettings();
      
      this.isSaving = false;

      if (typeof window !== 'undefined' && (window as any).showToast) {
        (window as any).showToast('success', 'Pomodoro ayarlarınız başarıyla uygulandı!');
      }
      this.cdr.detectChanges();
    }, 500);
  }
}
