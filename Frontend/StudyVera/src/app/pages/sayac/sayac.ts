import { Component, OnDestroy, signal, computed, Inject, PLATFORM_ID, OnInit, effect } from '@angular/core';
import { CommonModule, isPlatformBrowser, DOCUMENT } from '@angular/common';
import { Title, Meta } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AuthService } from '../../services/auth.service';
import { LessonService } from '../../services/lesson.service';
import { TopicService } from '../../services/topic.service';
import { UserLessonProgressService } from '../../services/user-lesson-progress.service';
import { UserQuestionStatsService } from '../../services/user-question-stats.service';

import { LessonDto } from '../../models/lesson.model';
import { TopicDto } from '../../models/topic.model';
import { AddUserLessonProgressDto } from '../../models/user-lesson-progress.model';
import { AddUserQuestionStatDto } from '../../models/user-question-stat.model';

export interface TimerStep {
  id: number;
  duration: number; // in seconds
  timestamp: number; // Date.now() when recorded
  isSaved?: boolean; // Whether it has been saved to the system
}

@Component({
  selector: 'app-sayac',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sayac.html',
  styleUrls: ['./sayac.css']
})
export class Sayac implements OnInit, OnDestroy {
  // State Signals
  currentSeconds = signal<number>(0);
  accumulatedTime = signal<number>(0);
  isRunning = signal<boolean>(false);
  startTime = signal<number>(0);
  steps = signal<TimerStep[]>([]);
  // --- Modal State ---
  isModalOpen = signal(false);
  isSubmitting = signal(false);
  errorMessage = signal('');
  currentStepToSave = signal<TimerStep | null>(null);

  activityType = signal<'lesson' | 'question'>('lesson');
  lessons = signal<LessonDto[]>([]);
  topics = signal<TopicDto[]>([]);

  selectedLessonId = signal<number>(0);
  selectedTopicId = signal<number>(0);

  // Soru çözümü alanları
  solvedCount = signal<number>(0);
  correctCount = signal<number>(0);

  private timerInterval: any = null;

  // Computed Values
  filteredTopics = computed(() => {
    const lid = this.selectedLessonId();
    if (lid === 0) return [];
    return this.topics().filter(t => t.lessonId === lid);
  });

  totalStepsTime = computed(() => {
    return this.steps().reduce((sum, step) => sum + step.duration, 0);
  });

  totalSeconds = computed(() => {
    return this.totalStepsTime() + this.currentSeconds();
  });

  timeDisplay = computed(() => {
    return this.formatSeconds(this.currentSeconds());
  });

  totalTimeDisplay = computed(() => {
    return this.formatSeconds(this.totalSeconds());
  });

  constructor(
    private titleService: Title,
    private metaService: Meta,
    @Inject(DOCUMENT) private document: Document,
    @Inject(PLATFORM_ID) private platformId: Object,
    public authService: AuthService,
    private lessonService: LessonService,
    private topicService: TopicService,
    private userLessonProgressService: UserLessonProgressService,
    private userQuestionStatsService: UserQuestionStatsService
  ) {
    if (isPlatformBrowser(this.platformId)) {
      // Dynamic page title sync effect
      effect(() => {
        const time = this.timeDisplay();
        const status = this.isRunning() ? "Çalışıyor" : "Durduruldu";
        this.titleService.setTitle(`(${time}) Sayaç | StudyVera`);
      });
    }
  }

  ngOnInit() {
    this.updateMeta();
    if (isPlatformBrowser(this.platformId)) {
      this.loadState();

      if (this.authService.isLoggedIn()) {
        this.loadLessonsAndTopics();
      }
    }
  }

  private loadLessonsAndTopics() {
    this.lessonService.getAll().subscribe({
      next: (data) => this.lessons.set(data),
      error: (err) => console.error('Dersleri yüklerken hata oluştu', err)
    });
    this.topicService.getTopics().subscribe({
      next: (data) => this.topics.set(data),
      error: (err) => console.error('Konuları yüklerken hata oluştu', err)
    });
  }

  onLessonChange(lessonIdStr: string) {
    const lessonId = parseInt(lessonIdStr, 10);
    this.selectedLessonId.set(lessonId);
    this.selectedTopicId.set(0);
  }

  private updateMeta() {
    this.titleService.setTitle('Sayaç | StudyVera');
    this.metaService.updateTag({ name: 'description', content: 'Çalışmalarınızı saniye saniye takip edin ve kaydedin.' });
  }

  // Load state from localStorage
  private loadState() {
    // 1. Load active stopwatch state
    const stateStr = localStorage.getItem('sayac_state');
    if (stateStr) {
      try {
        const state = JSON.parse(stateStr);
        this.accumulatedTime.set(state.accumulatedTime || 0);
        this.startTime.set(state.startTime || 0);
        this.isRunning.set(state.isRunning || false);
        this.currentSeconds.set(state.currentSeconds || 0);

        if (state.isRunning && state.startTime) {
          const now = Date.now();
          const elapsed = Math.round((now - state.startTime) / 1000);
          this.currentSeconds.set(state.accumulatedTime + elapsed);
          this.startTickLoop();
        }
      } catch (e) {
        console.error('Error loading sayac state', e);
      }
    }

    // 2. Load steps
    const stepsStr = localStorage.getItem('sayac_steps');
    if (stepsStr) {
      try {
        const parsed = JSON.parse(stepsStr);
        if (Array.isArray(parsed)) {
          this.steps.set(parsed);
        }
      } catch (e) {
        console.error('Error loading sayac steps', e);
      }
    }
  }

  // Save active stopwatch state to localStorage
  private saveState() {
    if (!isPlatformBrowser(this.platformId)) return;
    const state = {
      isRunning: this.isRunning(),
      startTime: this.startTime(),
      accumulatedTime: this.accumulatedTime(),
      currentSeconds: this.currentSeconds()
    };
    localStorage.setItem('sayac_state', JSON.stringify(state));
  }

  private startTickLoop() {
    if (this.timerInterval) clearInterval(this.timerInterval);

    this.timerInterval = setInterval(() => {
      const now = Date.now();
      const elapsed = Math.round((now - this.startTime()) / 1000);
      this.currentSeconds.set(this.accumulatedTime() + elapsed);
      this.saveState();
    }, 1000);
  }

  startTimer() {
    if (this.isRunning()) return;

    this.isRunning.set(true);
    this.startTime.set(Date.now());
    this.startTickLoop();
    this.saveState();
  }

  stopTimer() {
    if (!this.isRunning()) return;

    this.isRunning.set(false);
    this.accumulatedTime.set(this.currentSeconds());

    if (this.timerInterval) {
      clearInterval(this.timerInterval);
      this.timerInterval = null;
    }
    this.saveState();
  }

  saveStep() {
    if (this.currentSeconds() === 0) return;

    // Create a new step entry
    const newStep: TimerStep = {
      id: Date.now(),
      duration: this.currentSeconds(),
      timestamp: Date.now()
    };

    // Prepend to steps list (latest first)
    const updatedSteps = [newStep, ...this.steps()];
    this.steps.set(updatedSteps);
    localStorage.setItem('sayac_steps', JSON.stringify(updatedSteps));

    // Reset current active session to 0
    this.currentSeconds.set(0);
    this.accumulatedTime.set(0);

    if (this.isRunning()) {
      // If stopwatch is currently running, continue from new startTime
      this.startTime.set(Date.now());
    } else {
      this.startTime.set(0);
    }

    this.saveState();
  }

  resetTimer() {
    const confirmClear = confirm('Tüm sayaç verilerini ve kaydedilen seansları sıfırlamak istediğinize emin misiniz?');
    if (confirmClear) {
      this.isRunning.set(false);
      this.currentSeconds.set(0);
      this.accumulatedTime.set(0);
      this.startTime.set(0);
      this.steps.set([]);

      if (this.timerInterval) {
        clearInterval(this.timerInterval);
        this.timerInterval = null;
      }

      localStorage.removeItem('sayac_state');
      localStorage.removeItem('sayac_steps');
    }
  }

  deleteStep(id: number) {
    const updated = this.steps().filter(s => s.id !== id);
    this.steps.set(updated);
    localStorage.setItem('sayac_steps', JSON.stringify(updated));
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

  // --- Modal Methods ---
  openSaveModal(step: TimerStep) {
    this.currentStepToSave.set(step);
    this.errorMessage.set('');
    this.activityType.set('lesson');
    this.selectedLessonId.set(0);
    this.selectedTopicId.set(0);
    this.solvedCount.set(0);
    this.correctCount.set(0);
    this.isModalOpen.set(true);
  }

  closeModal() {
    this.isModalOpen.set(false);
    this.currentStepToSave.set(null);
  }

  setActivityType(type: 'lesson' | 'question') {
    this.activityType.set(type);
    this.errorMessage.set('');
  }

  submitActivity() {
    const step = this.currentStepToSave();
    if (!step) return;

    if (this.selectedTopicId() === 0) {
      this.errorMessage.set('Lütfen bir konu seçiniz.');
      return;
    }

    this.isSubmitting.set(true);
    const durationMinutes = Math.max(1, Math.round(step.duration / 60));

    if (this.activityType() === 'lesson') {
      const request: AddUserLessonProgressDto = {
        topicId: this.selectedTopicId(),
        durationMinutes: durationMinutes,
        progressStatus: 1
      };

      this.userLessonProgressService.add(request).subscribe({
        next: () => this.handleSaveSuccess(step.id),
        error: (err) => this.handleSaveError(err)
      });
    } else {
      if (this.solvedCount() <= 0) {
        this.errorMessage.set('Çözülen soru sayısı 0\'dan büyük olmalıdır.');
        this.isSubmitting.set(false);
        return;
      }
      if (this.correctCount() < 0 || this.correctCount() > this.solvedCount()) {
        this.errorMessage.set('Geçerli bir doğru sayısı giriniz.');
        this.isSubmitting.set(false);
        return;
      }

      const request: AddUserQuestionStatDto = {
        topicId: this.selectedTopicId(),
        solvedCount: this.solvedCount(),
        correctCount: this.correctCount(),
        durationMinutes: durationMinutes
      };

      this.userQuestionStatsService.add(request).subscribe({
        next: () => this.handleSaveSuccess(step.id),
        error: (err) => this.handleSaveError(err)
      });
    }
  }

  private handleSaveSuccess(stepId: number) {
    this.isSubmitting.set(false);
    this.closeModal();
    this.showToast('success', 'Çalışma başarıyla sisteme kaydedildi!');

    // Mark step as saved
    const updated = this.steps().map(s => s.id === stepId ? { ...s, isSaved: true } : s);
    this.steps.set(updated);
    localStorage.setItem('sayac_steps', JSON.stringify(updated));
  }

  private handleSaveError(err: any) {
    this.isSubmitting.set(false);
    this.errorMessage.set(err.error?.message || 'Kaydedilirken bir hata oluştu.');
    this.showToast('error', 'Kaydedilirken bir hata oluştu.');
  }

  private showToast(type: string, message: string) {
    if (typeof window !== 'undefined' && (window as any).showToast) {
      (window as any).showToast(type, message);
    }
  }

  // Formatting helper: returns HH:MM:SS
  formatSeconds(totalSecs: number): string {
    const hrs = Math.floor(totalSecs / 3600);
    const mins = Math.floor((totalSecs % 3600) / 60);
    const secs = totalSecs % 60;
    return `${hrs.toString().padStart(2, '0')}:${mins.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`;
  }

  // Duration formatting helper (e.g. "1 sa 15 dk 40 sn")
  formatDuration(totalSecs: number): string {
    const hrs = Math.floor(totalSecs / 3600);
    const mins = Math.floor((totalSecs % 3600) / 60);
    const secs = totalSecs % 60;

    let res = '';
    if (hrs > 0) res += `${hrs} sa `;
    if (mins > 0 || hrs > 0) res += `${mins} dk `;
    res += `${secs} sn`;
    return res;
  }

  formatTimestamp(timestamp: number): string {
    const date = new Date(timestamp);
    const timeStr = date.toLocaleTimeString('tr-TR', { hour: '2-digit', minute: '2-digit', second: '2-digit' });
    const dateStr = date.toLocaleDateString('tr-TR', { day: 'numeric', month: 'short' });
    return `${timeStr} (${dateStr})`;
  }

  ngOnDestroy() {
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
    }
    this.titleService.setTitle('StudyVera');
  }
}
