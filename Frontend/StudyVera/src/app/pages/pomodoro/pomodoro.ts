import { Component, OnDestroy, signal, computed, Inject, PLATFORM_ID, OnInit } from '@angular/core';
import { CommonModule, isPlatformBrowser, DOCUMENT } from '@angular/common';
import { Title, Meta } from '@angular/platform-browser';
import { interval, Subscription } from 'rxjs';
import { AudioService } from '../../services/audio.service';

export enum StudyMode { Quick = 0.1 , Deep = 50, Developer = 90 }

@Component({
  selector: 'app-pomodoro',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './pomodoro.html',
  styleUrls: ['./pomodoro.css']
})
export class Pomodoro implements OnInit, OnDestroy {
  // Enums for template
  studyModes = [StudyMode.Quick, StudyMode.Deep, StudyMode.Developer];
  
  // State
  currentMode = signal(StudyMode.Quick);
  remainingSeconds = signal(StudyMode.Quick * 60);
  isRunning = signal(false);
  isBreakMode = signal(false);
  isOverdue = signal(false);

  settings = {
    personalMessage: "Mola bitti, odaklanma zamanı!",
    isAudioAlertEnabled: true 
  };

  private timerSub?: Subscription;

  constructor(
    private audioService: AudioService,
    private titleService: Title,
    private metaService: Meta,
    @Inject(DOCUMENT) private document: Document,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  ngOnInit() {
    this.updateMeta();
    if (isPlatformBrowser(this.platformId)) {
      const saved = localStorage.getItem('pomodoro_settings');
      if (saved) this.settings = JSON.parse(saved);
    }
  }

  timeDisplay = computed(() => {
    const absSeconds = Math.abs(this.remainingSeconds());
    const mins = Math.floor(absSeconds / 60);
    const secs = absSeconds % 60;
    const sign = this.remainingSeconds() < 0 ? "-" : "";
    return `${sign}${mins.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`;
  });

  molaSuresiDakika = computed(() => {
    if (this.currentMode() === StudyMode.Quick) return 0.1;
    if (this.currentMode() === StudyMode.Deep) return 10;
    return 20;
  });

  pageTitleText = computed(() => this.isBreakMode() ? "Mola Zamanı | StudyVera" : "Pomodoro | StudyVera");

  setMode(mode: StudyMode) {
    this.stopTimer();
    this.isBreakMode.set(false);
    this.isOverdue.set(false);
    this.currentMode.set(mode);
    this.remainingSeconds.set(mode * 60);
    this.updateMeta();
  }

  startTimer() {
    if (this.isRunning()) return;
    this.isRunning.set(true);
    
    this.timerSub = interval(1000).subscribe(() => {
      this.remainingSeconds.update(s => s - 1);
      
      if (this.remainingSeconds() === 0) {
        this.stopTimer();
        this.isBreakMode.set(true);
        this.updateMeta();
      }
    });
  }

  startBreak() {
    this.stopTimer();
    this.remainingSeconds.set(this.molaSuresiDakika() * 60);
    this.isRunning.set(true);

    this.timerSub = interval(1000).subscribe(() => {
      this.remainingSeconds.update(s => s - 1);

      if (this.remainingSeconds() === 0 && this.settings.isAudioAlertEnabled) {
        this.audioService.playNotification(this.settings.personalMessage);
      }

      if (this.remainingSeconds() < 0) {
        this.isOverdue.set(true);
      }
    });
  }

  endBreak() {
    this.stopTimer();
    this.isBreakMode.set(false);
    this.isOverdue.set(false);
    this.remainingSeconds.set(this.currentMode() * 60);
    this.updateMeta();
  }

  stopTimer() {
    this.timerSub?.unsubscribe();
    this.isRunning.set(false);
  }

  resetTimer() {
    this.endBreak();
  }

  toggleFullScreen(element: HTMLElement) {
    if (!this.document.fullscreenElement) {
      element.requestFullscreen().catch(err => {
        console.error(`Tam ekran hatası: ${err.message}`);
      });
    } else {
      this.document.exitFullscreen();
    }
  }

  getModeName(mode: StudyMode): string {
    switch (mode) {
      case StudyMode.Quick: return "Hızlı";
      case StudyMode.Deep: return "Derin";
      case StudyMode.Developer: return "Geliştirici";
      default: return "Bilinmeyen";
    }
  }

  private updateMeta() {
    this.titleService.setTitle(this.pageTitleText());
    this.metaService.updateTag({ name: 'description', content: 'Verimliliğinizi artıracak en sade Pomodoro sayacı...' });
  }

  ngOnDestroy() {
    this.stopTimer();
  }
}