import { Injectable, signal, PLATFORM_ID, Inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { AudioService } from './audio.service';

export enum StudyMode { Quick = 0.1, Deep = 50, Developer = 90 }

@Injectable({ providedIn: 'root' })
export class PomodoroService {
  // State Signals
  currentMode = signal<number>(StudyMode.Quick);
  remainingSeconds = signal<number>(StudyMode.Quick * 60);
  isRunning = signal<boolean>(false);
  isBreakMode = signal<boolean>(false);
  isOverdue = signal<boolean>(false);
  customModes = signal<number[]>([]);
  sessionEndedRecently = signal<{ modeMinutes: number } | null>(null);

  settings = {
    personalMessage: "Haydi derse",
    isAudioAlertEnabled: true,
    bgColor: "",
    customMinutes: 50,
    saveCustomMinutes: false,
    customBgColors: [] as string[]
  };

  private timerInterval: any = null;
  private hasPlayedNotification = false;

  constructor(
    private audioService: AudioService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    if (isPlatformBrowser(this.platformId)) {
      this.loadSettings();
      this.loadCustomModes();
      this.loadTimerState();
    }
  }

  // Cookie helpers
  private setCookie(name: string, value: string, days = 365) {
    if (!isPlatformBrowser(this.platformId)) return;
    const date = new Date();
    date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
    const expires = "; expires=" + date.toUTCString();
    document.cookie = name + "=" + encodeURIComponent(value) + expires + "; path=/; SameSite=Lax";
  }

  private getCookie(name: string): string | null {
    if (!isPlatformBrowser(this.platformId)) return null;
    const nameEQ = name + "=";
    const ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) === ' ') c = c.substring(1, c.length);
      if (c.indexOf(nameEQ) === 0) return decodeURIComponent(c.substring(nameEQ.length, c.length));
    }
    return null;
  }

  private deleteCookie(name: string) {
    if (!isPlatformBrowser(this.platformId)) return;
    document.cookie = name + "=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;";
  }

  private loadSettings() {
    const saved = this.getCookie('pomodoro_settings') || localStorage.getItem('pomodoro_settings');
    if (saved) {
      try {
        const parsed = JSON.parse(saved);
        this.settings = { ...this.settings, ...parsed };
        if (!this.settings.customBgColors) {
          this.settings.customBgColors = [];
        }
      } catch (e) {
        console.error('Error parsing pomodoro settings', e);
      }
    }
  }

  saveSettings() {
    if (isPlatformBrowser(this.platformId)) {
      const val = JSON.stringify(this.settings);
      localStorage.setItem('pomodoro_settings', val);
      this.setCookie('pomodoro_settings', val);
    }
  }

  // Custom Modes persistence
  private loadCustomModes() {
    const saved = this.getCookie('pomodoro_custom_modes') || localStorage.getItem('pomodoro_custom_modes');
    if (saved) {
      try {
        const parsed = JSON.parse(saved);
        if (Array.isArray(parsed)) {
          this.customModes.set(parsed);
        }
      } catch (e) {
        console.error('Error loading custom modes', e);
      }
    }
  }

  private saveCustomModes() {
    if (isPlatformBrowser(this.platformId)) {
      const val = JSON.stringify(this.customModes());
      localStorage.setItem('pomodoro_custom_modes', val);
      this.setCookie('pomodoro_custom_modes', val);
    }
  }

  addCustomMode(mins: number, saveToBrowser: boolean) {
    if (!this.customModes().includes(mins)) {
      const updated = [...this.customModes(), mins].sort((a, b) => a - b);
      this.customModes.set(updated);

      if (saveToBrowser) {
        this.saveCustomModes();
      }
    }
  }

  removeCustomMode(mins: number) {
    const updated = this.customModes().filter(m => m !== mins);
    this.customModes.set(updated);
    this.saveCustomModes();

    // If active mode is the one removed, fallback to Quick
    if (this.currentMode() === mins) {
      this.setMode(StudyMode.Quick);
    }
  }

  getMolaSuresiDakika(mode: number): number {
    if (mode === StudyMode.Quick) return 0.1;
    if (mode === StudyMode.Deep) return 10;
    if (mode === StudyMode.Developer) return 20;
    // Custom break duration
    return Math.max(5, Math.round(mode * 0.2));
  }

  private loadTimerState() {
    const stateStr = this.getCookie('pomodoro_timer_state') || localStorage.getItem('pomodoro_timer_state');
    if (stateStr) {
      try {
        const state = JSON.parse(stateStr);
        this.currentMode.set(state.currentMode);
        this.isBreakMode.set(state.isBreakMode);
        this.isOverdue.set(state.isOverdue || false);
        this.hasPlayedNotification = state.hasPlayedNotification || false;

        if (state.isRunning && state.endTime) {
          const now = Date.now();
          if (state.isBreakMode) {
            const diff = Math.round((state.endTime - now) / 1000);
            this.remainingSeconds.set(diff);
            this.isRunning.set(true);
            this.startTickLoop(state.endTime);
          } else {
            const diff = Math.round((state.endTime - now) / 1000);
            if (diff <= 0) {
              this.isBreakMode.set(true);
              this.isRunning.set(false);
              this.isOverdue.set(false);
              this.remainingSeconds.set(this.getMolaSuresiDakika(state.currentMode) * 60);
              this.saveTimerState();
            } else {
              this.remainingSeconds.set(diff);
              this.isRunning.set(true);
              this.startTickLoop(state.endTime);
            }
          }
        } else {
          this.remainingSeconds.set(state.remainingSeconds);
          this.isRunning.set(false);
        }
      } catch (e) {
        console.error('Error loading pomodoro timer state', e);
      }
    }
  }

  saveTimerState(endTime: number | null = null) {
    if (!isPlatformBrowser(this.platformId)) return;

    const state = {
      isRunning: this.isRunning(),
      endTime: endTime,
      isBreakMode: this.isBreakMode(),
      isOverdue: this.isOverdue(),
      currentMode: this.currentMode(),
      remainingSeconds: this.remainingSeconds(),
      hasPlayedNotification: this.hasPlayedNotification
    };
    const val = JSON.stringify(state);
    localStorage.setItem('pomodoro_timer_state', val);
    this.setCookie('pomodoro_timer_state', val);
  }

  private startTickLoop(endTime: number) {
    if (this.timerInterval) clearInterval(this.timerInterval);

    this.timerInterval = setInterval(() => {
      const now = Date.now();

      if (!this.isBreakMode()) {
        const diff = Math.round((endTime - now) / 1000);
        if (diff <= 0) {
          const finishedMode = this.currentMode();
          this.remainingSeconds.set(0);
          this.stopTimer();
          this.isBreakMode.set(true);
          this.remainingSeconds.set(this.getMolaSuresiDakika(finishedMode) * 60);
          this.sessionEndedRecently.set({ modeMinutes: finishedMode });
          this.saveTimerState();
        } else {
          this.remainingSeconds.set(diff);
        }
      } else {
        const diff = Math.round((endTime - now) / 1000);
        this.remainingSeconds.set(diff);

        if (diff <= 0) {
          this.isOverdue.set(true);
          if (!this.hasPlayedNotification) {
            this.hasPlayedNotification = true;
            if (this.settings.isAudioAlertEnabled) {
              this.audioService.playNotification(this.settings.personalMessage);
            }
          }
        }
      }

      this.saveTimerState(endTime);
    }, 1000);
  }

  setMode(mode: number) {
    this.stopTimer();
    this.isBreakMode.set(false);
    this.isOverdue.set(false);
    this.currentMode.set(mode);
    this.remainingSeconds.set(mode * 60);
    this.hasPlayedNotification = false;
    this.saveTimerState();
  }

  startTimer() {
    if (this.isRunning()) return;
    this.isRunning.set(true);
    this.hasPlayedNotification = false;

    const endTime = Date.now() + this.remainingSeconds() * 1000;
    this.startTickLoop(endTime);
    this.saveTimerState(endTime);
  }

  startBreak() {
    this.stopTimer();
    this.isBreakMode.set(true);
    this.isOverdue.set(false);
    this.hasPlayedNotification = false;

    const breakDuration = this.getMolaSuresiDakika(this.currentMode()) * 60;
    this.remainingSeconds.set(breakDuration);
    this.isRunning.set(true);

    const endTime = Date.now() + breakDuration * 1000;
    this.startTickLoop(endTime);
    this.saveTimerState(endTime);
  }

  endBreak() {
    this.stopTimer();
    this.isBreakMode.set(false);
    this.isOverdue.set(false);
    this.hasPlayedNotification = false;
    this.remainingSeconds.set(this.currentMode() * 60);
    this.saveTimerState();
  }

  stopTimer() {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
      this.timerInterval = null;
    }
    this.isRunning.set(false);
    this.saveTimerState();
  }

  resetTimer() {
    this.endBreak();
  }
}
