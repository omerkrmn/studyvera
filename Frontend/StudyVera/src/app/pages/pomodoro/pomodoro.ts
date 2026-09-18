import { Component, OnDestroy, signal, computed, Inject, PLATFORM_ID, OnInit, effect } from '@angular/core';
import { CommonModule, isPlatformBrowser, DOCUMENT } from '@angular/common';
import { Title, Meta } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { PomodoroService, StudyMode } from '../../services/pomodoro.service';
import { LessonService } from '../../services/lesson.service';
import { TopicService } from '../../services/topic.service';
import { UserLessonProgressService } from '../../services/user-lesson-progress.service';
import { UserQuestionStatsService } from '../../services/user-question-stats.service';
import { LessonDto } from '../../models/lesson.model';
import { TopicDto } from '../../models/topic.model';
import { ProgressStatus } from '../../models/user-lesson-progress.model';

export interface PendingActivity {
  id: string;
  type: 'lesson' | 'question';
  lessonId: number;
  topicId: number;
  lessonName: string;
  topicName: string;
  durationMinutes: number;
  solvedCount?: number;
  correctCount?: number;
}

@Component({
  selector: 'app-pomodoro',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pomodoro.html',
  styleUrls: ['./pomodoro.css']
})
export class Pomodoro implements OnInit, OnDestroy {
  // Enums for template
  studyModes = [StudyMode.Quick, StudyMode.Deep, StudyMode.Developer];
  
  // Modal State
  isModalOpen = false;
  newModeMinutes = 45;
  saveNewModeCheckbox = true;

  backgroundColors = [
    { name: 'Varsayılan', value: '' },
    { name: 'Koyu Gece', value: '#1a1a2e' },
    { name: 'Orman', value: '#1b4332' },
    { name: 'Derin Mavi', value: '#0f2027' },
    { name: 'Gün Batımı', value: '#4a154b' }
  ];

  isSessionEndModalOpen = false;
  completedSessionMinutes = 0;
  
  // Pending Activities State
  pendingActivities: PendingActivity[] = [];
  currentActivityDuration = 0;
  
  activityType: 'lesson' | 'question' = 'lesson';
  lessons: LessonDto[] = [];
  allTopics: TopicDto[] = [];
  topics: TopicDto[] = [];
  selectedLessonId?: number;
  selectedTopicId?: number;
  solvedCount: number = 0;
  correctCount: number = 0;
  isSubmittingActivity = false;

  constructor(
    public pomodoroService: PomodoroService,
    private lessonService: LessonService,
    private topicService: TopicService,
    private userLessonProgressService: UserLessonProgressService,
    private userQuestionStatsService: UserQuestionStatsService,
    private titleService: Title,
    private metaService: Meta,
    @Inject(DOCUMENT) private document: Document,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    if (isPlatformBrowser(this.platformId)) {
      // Dynamic page title sync effect
      effect(() => {
        const time = this.timeDisplay();
        const modeText = this.pomodoroService.isBreakMode() ? "Mola" : "Çalışma";
        this.titleService.setTitle(`(${time}) ${modeText} | StudyVera`);
      });

      effect(() => {
        const sessionData = this.pomodoroService.sessionEndedRecently();
        if (sessionData) {
          setTimeout(() => {
            this.completedSessionMinutes = Math.round(sessionData.modeMinutes);
            this.currentActivityDuration = this.completedSessionMinutes;
            this.isSessionEndModalOpen = true;
            this.loadActivityData();
          });
        }
      });
    }
  }

  ngOnInit() {
    this.updateMeta();
  }

  timeDisplay = computed(() => {
    const absSeconds = Math.abs(this.pomodoroService.remainingSeconds());
    const mins = Math.floor(absSeconds / 60);
    const secs = absSeconds % 60;
    const sign = this.pomodoroService.remainingSeconds() < 0 ? "-" : "";
    return `${sign}${mins.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`;
  });

  isBreakMode = computed(() => this.pomodoroService.isBreakMode());
  isRunning = computed(() => this.pomodoroService.isRunning());
  isOverdue = computed(() => this.pomodoroService.isOverdue());
  currentMode = computed(() => this.pomodoroService.currentMode());

  get settings() {
    return this.pomodoroService.settings;
  }

  molaSuresiDakika = computed(() => {
    return this.pomodoroService.getMolaSuresiDakika(this.pomodoroService.currentMode());
  });

  isCustomModeActive = computed(() => {
    const mode = this.pomodoroService.currentMode();
    return mode !== StudyMode.Quick && mode !== StudyMode.Deep && mode !== StudyMode.Developer;
  });

  setMode(mode: number) {
    this.pomodoroService.setMode(mode);
  }

  startTimer() {
    this.pomodoroService.startTimer();
  }

  startBreak() {
    this.pomodoroService.startBreak();
  }

  endBreak() {
    this.pomodoroService.endBreak();
  }

  stopTimer() {
    this.pomodoroService.stopTimer();
  }

  resetTimer() {
    this.pomodoroService.resetTimer();
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

  getModeName(mode: number): string {
    switch (mode) {
      case StudyMode.Quick: return "Hızlı";
      case StudyMode.Deep: return "Derin";
      case StudyMode.Developer: return "Geliştirici";
      default: return `Özel`;
    }
  }

  setBgColor(color: string) {
    this.pomodoroService.settings.bgColor = color;
    this.pomodoroService.saveSettings();
  }

  addCustomColor(color: string) {
    if (!this.settings.customBgColors) {
      this.settings.customBgColors = [];
    }
    if (!this.settings.customBgColors.includes(color) && !this.backgroundColors.find(c => c.value === color)) {
      this.settings.customBgColors.push(color);
    }
    this.setBgColor(color);
  }

  removeCustomColor(color: string) {
    if (this.settings.customBgColors) {
      this.settings.customBgColors = this.settings.customBgColors.filter(c => c !== color);
      if (this.settings.bgColor === color) {
        this.setBgColor(''); // fallback to default
      } else {
        this.pomodoroService.saveSettings();
      }
    }
  }

  // Modal Actions
  openAddModeModal() {
    this.isModalOpen = true;
  }

  closeAddModeModal() {
    this.isModalOpen = false;
  }

  saveNewCustomMode() {
    const mins = Math.max(1, Math.min(300, this.newModeMinutes));
    this.newModeMinutes = mins;
    this.pomodoroService.addCustomMode(mins, this.saveNewModeCheckbox);
    this.pomodoroService.setMode(mins);
    this.closeAddModeModal();
  }

  removeCustomMode(mins: number) {
    this.pomodoroService.removeCustomMode(mins);
  }

  private updateMeta() {
    const titleText = this.pomodoroService.isBreakMode() ? "Mola Zamanı | StudyVera" : "Pomodoro | StudyVera";
    this.titleService.setTitle(titleText);
    this.metaService.updateTag({ name: 'description', content: 'Verimliliğinizi artıracak en sade Pomodoro sayacı...' });
  }

  // Activity Tracking Logic
  loadActivityData() {
    this.lessonService.getAll().subscribe(res => this.lessons = res);
    this.topicService.getTopics().subscribe(res => this.allTopics = res);
  }

  onLessonChange() {
    this.selectedTopicId = undefined;
    if (this.selectedLessonId) {
      this.topics = this.allTopics.filter(t => t.lessonId == this.selectedLessonId);
    } else {
      this.topics = [];
    }
  }

  get remainingSessionMinutes(): number {
    const used = this.pendingActivities.reduce((sum, act) => sum + act.durationMinutes, 0);
    return Math.max(0, this.completedSessionMinutes - used);
  }

  addPendingActivity() {
    if (!this.selectedTopicId || !this.selectedLessonId) {
      this.showToast('Lütfen ders ve konu seçiniz.');
      return;
    }
    if (this.currentActivityDuration <= 0) {
      this.showToast('Lütfen geçerli bir süre giriniz.');
      return;
    }
    if (this.currentActivityDuration > this.remainingSessionMinutes) {
      this.showToast(`Süre kalan süreden (${this.remainingSessionMinutes} dk) fazla olamaz.`);
      return;
    }
    if (this.activityType === 'question' && this.correctCount > this.solvedCount) {
      this.showToast('Doğru sayısı çözülen sorudan büyük olamaz.');
      return;
    }

    const lesson = this.lessons.find(l => l.id == this.selectedLessonId);
    const topic = this.topics.find(t => t.id == this.selectedTopicId);

    const activity: PendingActivity = {
      id: Date.now().toString() + Math.random().toString(),
      type: this.activityType,
      lessonId: Number(this.selectedLessonId),
      topicId: Number(this.selectedTopicId),
      lessonName: lesson?.name || 'Bilinmiyor',
      topicName: topic?.name || 'Bilinmiyor',
      durationMinutes: this.currentActivityDuration,
      solvedCount: this.activityType === 'question' ? this.solvedCount : undefined,
      correctCount: this.activityType === 'question' ? this.correctCount : undefined
    };

    this.pendingActivities.push(activity);
    
    // Reset inputs for next activity
    this.solvedCount = 0;
    this.correctCount = 0;
    this.currentActivityDuration = this.remainingSessionMinutes;
  }

  removePendingActivity(id: string) {
    this.pendingActivities = this.pendingActivities.filter(a => a.id !== id);
    this.currentActivityDuration = this.remainingSessionMinutes;
  }

  showToast(message: string) {
    alert(message);
  }

  closeSessionEndModal() {
    this.isSessionEndModalOpen = false;
    this.pomodoroService.sessionEndedRecently.set(null);
    this.pendingActivities = [];
    this.selectedLessonId = undefined;
    this.selectedTopicId = undefined;
    this.solvedCount = 0;
    this.correctCount = 0;
  }

  submitActivity() {
    // If pending is empty but form has data, add it first automatically
    if (this.pendingActivities.length === 0 && this.selectedTopicId && this.currentActivityDuration > 0) {
      this.addPendingActivity();
      if (this.pendingActivities.length === 0) return; // Add failed (validation)
    }

    if (this.pendingActivities.length === 0) {
      this.showToast('Lütfen en az bir aktivite ekleyiniz veya doldurunuz.');
      return;
    }

    this.isSubmittingActivity = true;
    
    const tasks: any[] = [];

    // Group question activities for bulk request
    const questionActivities = this.pendingActivities.filter(a => a.type === 'question');
    if (questionActivities.length > 0) {
      const questionRequests = questionActivities.map(act => ({
        topicId: act.topicId,
        solvedCount: act.solvedCount || 0,
        correctCount: act.correctCount || 0,
        durationMinutes: act.durationMinutes
      }));
      tasks.push(this.userQuestionStatsService.addRange(questionRequests));
    }

    // Process lesson activities individually
    const lessonActivities = this.pendingActivities.filter(a => a.type === 'lesson');
    lessonActivities.forEach(act => {
      tasks.push(this.userLessonProgressService.add({
        topicId: act.topicId,
        progressStatus: ProgressStatus.InProgress,
        durationMinutes: act.durationMinutes
      }));
    });

    if (tasks.length === 0) {
       this.isSubmittingActivity = false;
       this.closeSessionEndModal();
       return;
    }

    forkJoin(tasks).subscribe({
      next: () => {
        this.isSubmittingActivity = false;
        this.closeSessionEndModal();
      },
      error: () => {
        this.isSubmittingActivity = false;
        this.showToast('Aktiviteler kaydedilirken bir hata oluştu.');
      }
    });
  }

  ngOnDestroy() {
    // Reset browser tab title on navigating away
    this.titleService.setTitle('StudyVera');
  }
}