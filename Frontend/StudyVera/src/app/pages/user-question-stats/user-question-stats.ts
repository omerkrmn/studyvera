import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { forkJoin } from 'rxjs';

import { UserQuestionStatsService } from '../../services/user-question-stats.service';
import { QuestionStatDetailService } from '../../services/question-stat-detail.service';
import { LessonService } from '../../services/lesson.service';
import { TopicService } from '../../services/topic.service';

import { UserQuestionStatDto, AddUserQuestionStatDto } from '../../models/user-question-stat.model';
import { LessonDto } from '../../models/lesson.model';
import { TopicDto } from '../../models/topic.model';

interface GroupedQuestions {
  lessonId: number;
  lessonName: string;
  items: UserQuestionStatDto[];
  totalSolved: number;
  totalCorrect: number;
  totalWrong: number;
  totalTimeSpentInMinutes: number;
  accuracy: number;
  lastAttempt: string;
}

@Component({
  selector: 'app-user-question-stats',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './user-question-stats.html',
  styleUrl: './user-question-stats.css',
})
export class UserQuestionStats implements OnInit {
  private titleService = inject(Title);
  private questionStatsService = inject(UserQuestionStatsService);
  private questionStatDetailService = inject(QuestionStatDetailService);
  private lessonService = inject(LessonService);
  private topicService = inject(TopicService);

  // --- Data State ---
  questions = signal<UserQuestionStatDto[]>([]);
  lessons = signal<LessonDto[]>([]);
  topics = signal<TopicDto[]>([]);
  isLoading = signal(true);

  // --- Filter State (list) ---
  searchText = signal('');
  showWeakOnly = signal(false);

  isModalOpen = signal(false);
  isEditModalOpen = signal(false);
  errorMessage = signal('');
  newQuestion = signal<AddUserQuestionStatDto>({ topicId: 0, solvedCount: 0, correctCount: 0 });
  editQuestion = signal<any>({ id: 0, solvedCount: 0, correctCount: 0, durationMinutes: null });

  // --- Modal Internal Filter State ---
  selectedLessonId = signal(0);
  searchTerm = signal('');
  showDropdown = signal(false);
  isSaving = signal(false);

  // --- Table Expand State ---
  expandedLessonIds = signal(new Set<number>());
  expandedTopicId = signal<number | null>(null);

  // --- Computed ---
  totalSolved = computed(() => this.questions().reduce((s, q) => s + q.totalSolvedCount, 0));

  totalAccuracy = computed(() => {
    const total = this.totalSolved();
    if (total === 0) return '0.0';
    const correct = this.questions().reduce((s, q) => s + q.totalCorrectCount, 0);
    return ((correct / total) * 100).toFixed(1);
  });

  favoriteLessonName = computed(() => {
    const qs = this.questions();
    if (!qs.length) return '-';
    const grouped = new Map<number, number>();
    qs.forEach(q => {
      const lid = q.topic?.lessonId ?? 0;
      grouped.set(lid, (grouped.get(lid) ?? 0) + q.totalSolvedCount);
    });
    let maxId = 0, maxVal = 0;
    grouped.forEach((val, key) => { if (val > maxVal) { maxVal = val; maxId = key; } });
    if (maxVal === 0) return '-';
    return this.lessons().find(l => l.id === maxId)?.name ?? 'Bilinmiyor';
  });

  filteredTopics = computed(() => {
    const lid = this.selectedLessonId();
    const term = this.searchTerm().toLowerCase();
    return this.topics().filter(t =>
      (lid === 0 || t.lessonId === lid) &&
      (term === '' || t.name.toLowerCase().includes(term))
    );
  });

  filteredGroupedQuestions = computed((): GroupedQuestions[] => {
    const qs = this.questions();
    const searchText = this.searchText().toLowerCase();
    const weakOnly = this.showWeakOnly();
    const lessonMap = this.buildLessonMap();

    let filtered = qs;

    if (searchText) {
      filtered = filtered.filter(q =>
        q.topic?.name?.toLowerCase().includes(searchText) ||
        (lessonMap.get(q.topic?.lessonId ?? 0) ?? '').toLowerCase().includes(searchText)
      );
    }

    if (weakOnly) {
      filtered = filtered.filter(q =>
        this.calculateDeficiencyScore(q.totalSolvedCount, q.totalCorrectCount, q.lastAttemptAt, q.topicId) > 100
      );
    }

    const groupMap = new Map<number, UserQuestionStatDto[]>();
    filtered.forEach(q => {
      const lid = q.topic?.lessonId ?? 0;
      if (!groupMap.has(lid)) groupMap.set(lid, []);
      groupMap.get(lid)!.push(q);
    });

    const result: GroupedQuestions[] = [];
    groupMap.forEach((items, lessonId) => {
      const totalSolved = items.reduce((s, i) => s + i.totalSolvedCount, 0);
      const totalCorrect = items.reduce((s, i) => s + i.totalCorrectCount, 0);
      const totalWrong = items.reduce((s, i) => s + i.totalWrongCount, 0);
      const totalTimeSpentInMinutes = items.reduce((s, i) => s + (i.totalTimeSpentInMinutes || 0), 0);
      const accuracy = totalSolved === 0 ? 0 : totalCorrect / totalSolved;
      const lastAttempt = items.reduce((max, i) =>
        new Date(i.lastAttemptAt) > new Date(max) ? i.lastAttemptAt : max, items[0].lastAttemptAt);
      result.push({
        lessonId,
        lessonName: lessonMap.get(lessonId) ?? `Ders #${lessonId}`,
        items,
        totalSolved,
        totalCorrect,
        totalWrong,
        totalTimeSpentInMinutes,
        accuracy,
        lastAttempt
      });
    });

    return result.sort((a, b) => a.lessonId - b.lessonId);
  });

  ngOnInit(): void {
    this.titleService.setTitle('Soru Çözme İstatistikleri - StudyVera');
    this.loadData();
  }

  loadData(): void {
    this.isLoading.set(true);
    forkJoin({
      questions: this.questionStatsService.getAll(),
      lessons: this.lessonService.getAll(),
      topics: this.topicService.getTopics()
    }).subscribe({
      next: ({ questions, lessons, topics }) => {
        this.questions.set(questions);
        this.lessons.set(lessons);
        this.topics.set(topics);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Veri yükleme hatası:', err);
        this.isLoading.set(false);
      }
    });
  }

  // --- Modal ---
  openModalForNew(): void {
    this.newQuestion.set({ topicId: 0, solvedCount: 0, correctCount: 0 });
    this.selectedLessonId.set(0);
    this.searchTerm.set('');
    this.showDropdown.set(false);
    this.errorMessage.set('');
    this.isModalOpen.set(true);
  }

  closeModal(): void {
    this.isModalOpen.set(false);
  }

  onLessonChanged(event: Event): void {
    const id = parseInt((event.target as HTMLSelectElement).value, 10);
    this.selectedLessonId.set(id);
    this.searchTerm.set('');
    this.newQuestion.update(q => ({ ...q, topicId: 0 }));
  }

  selectTopic(topic: TopicDto): void {
    this.newQuestion.update(q => ({ ...q, topicId: topic.id }));
    this.searchTerm.set(topic.name);
    this.showDropdown.set(false);
  }

  onSearchTermInput(value: string): void {
    this.searchTerm.set(value);
    this.showDropdown.set(true);
  }

  hideDropdownDelayed(): void {
    setTimeout(() => this.showDropdown.set(false), 200);
  }

  async processAddQuestion(shouldClose: boolean): Promise<void> {
    const q = this.newQuestion();

    if (q.topicId === 0) {
      this.errorMessage.set('Lütfen bir konu seçiniz.');
      this.showToast('error', 'Lütfen bir konu seçiniz.');
      return;
    }
    if (q.solvedCount < q.correctCount) {
      this.errorMessage.set('Doğru sayısı toplam sorudan fazla olamaz!');
      this.showToast('error', 'Doğru sayısı toplam sorudan fazla olamaz!');
      return;
    }

    this.isSaving.set(true);
    this.questionStatsService.add(q).subscribe({
      next: () => {
        this.questionStatsService.getAll().subscribe(updated => {
          this.questions.set(updated);
          this.showToast('success', 'İlerleme başarıyla kaydedildi! 🎉');
          this.errorMessage.set('');
          this.isSaving.set(false);

          if (shouldClose) {
            this.isModalOpen.set(false);
          } else {
            this.newQuestion.set({ topicId: 0, solvedCount: 0, correctCount: 0 });
            this.searchTerm.set('');
            this.selectedLessonId.set(0);
          }
        });
      },
      error: (err) => {
        const msg = err.error?.message || 'Bir hata oluştu.';
        this.showToast('error', msg);
        this.isSaving.set(false);
      }
    });
  }

  saveAndClose(): void { this.processAddQuestion(true); }
  saveAndNew(): void { this.processAddQuestion(false); }

  // --- Edit/Delete Detail ---
  openEditModal(detail: any): void {
    this.editQuestion.set({
      id: detail.id,
      solvedCount: detail.solvedCount,
      correctCount: detail.correctCount,
      durationMinutes: detail.durationMinutes
    });
    this.errorMessage.set('');
    this.isEditModalOpen.set(true);
  }

  closeEditModal(): void {
    this.isEditModalOpen.set(false);
  }

  saveEdit(): void {
    const q = this.editQuestion();
    if (q.solvedCount < q.correctCount) {
      this.errorMessage.set('Doğru sayısı toplamdan fazla olamaz!');
      this.showToast('error', 'Doğru sayısı toplamdan fazla olamaz!');
      return;
    }
    this.isSaving.set(true);
    this.questionStatsService.updateDetail(q.id, {
      solvedCount: q.solvedCount,
      correctCount: q.correctCount,
      durationMinutes: q.durationMinutes
    }).subscribe({
      next: () => {
        this.isSaving.set(false);
        this.isEditModalOpen.set(false);
        this.showToast('success', 'Kayıt güncellendi!');
        this.expandedTopicId.set(null); // Collapse so user can reopen to fetch latest
        this.loadData();
      },
      error: (err) => {
        this.isSaving.set(false);
        const msg = err.error?.message || 'Güncelleme başarısız oldu.';
        this.errorMessage.set(msg);
        this.showToast('error', msg);
      }
    });
  }

  deleteDetail(id: number): void {
    if (!confirm('Bu kaydı silmek istediğinize emin misiniz? İşlem geri alınamaz.')) return;
    
    this.questionStatsService.deleteDetail(id).subscribe({
      next: () => {
        this.showToast('success', 'Kayıt başarıyla silindi!');
        this.expandedTopicId.set(null); // Collapse to trigger refetch on next open
        this.loadData();
      },
      error: (err) => {
        const msg = err.error?.message || 'Silme işlemi başarısız oldu.';
        this.showToast('error', msg);
      }
    });
  }

  // --- Table expand ---
  toggleLesson(lessonId: number): void {
    const set = new Set(this.expandedLessonIds());
    if (set.has(lessonId)) set.delete(lessonId);
    else set.add(lessonId);
    this.expandedLessonIds.set(set);
  }

  isLessonExpanded(lessonId: number): boolean {
    return this.expandedLessonIds().has(lessonId);
  }

  toggleTopicExpand(question: UserQuestionStatDto): void {
    if (this.expandedTopicId() === question.id) {
      this.expandedTopicId.set(null);
      return;
    }
    this.expandedTopicId.set(question.id);
    if (!question.questionStatDetail || question.questionStatDetail.length === 0) {
      this.questionStatDetailService.getAll(question.id).subscribe(details => {
        const all = this.questions();
        const idx = all.findIndex(q => q.id === question.id);
        if (idx !== -1) {
          const updated = [...all];
          updated[idx] = { ...updated[idx], questionStatDetail: details };
          this.questions.set(updated);
        }
      });
    }
  }

  // --- Helpers ---
  buildLessonMap(): Map<number, string> {
    const m = new Map<number, string>();
    this.lessons().forEach(l => m.set(l.id, l.name));
    return m;
  }

  getColorByRate(rate: number): string {
    if (rate >= 0.75) return 'bg-success';
    if (rate >= 0.50) return 'bg-warning text-dark';
    return 'bg-danger';
  }

  getBadgeClass(accuracy: number): string {
    if (accuracy >= 0.75) return 'text-bg-success';
    if (accuracy >= 0.50) return 'text-bg-warning';
    return 'text-bg-danger';
  }

  calculateDeficiencyScore(totalSolvedCount: number, correctCount: number, lastAttemptAt: string, topicId: number): number {
    const topicPriority = this.topics().find(t => t.id === topicId)?.priority ?? 1;

    const expectedSuccessRate = 0.60;
    const successWeight = 0.7;
    const recencyWeight = 0.3;
    const confidenceThreshold = 10;
    const recencyLimitDays = 90;

    const daysSinceLastAttempt = Math.floor(
      (Date.now() - new Date(lastAttemptAt).getTime()) / (1000 * 60 * 60 * 24)
    );
    const recencyScore = Math.min(daysSinceLastAttempt, recencyLimitDays);

    const adjustedSuccessRatio = (correctCount + confidenceThreshold * expectedSuccessRate) /
      (totalSolvedCount + confidenceThreshold);

    const successPercentage = adjustedSuccessRatio * 100;
    const successLossScore = 100 - successPercentage;

    return ((successWeight * successLossScore) + (recencyWeight * recencyScore)) * topicPriority;
  }

  formatDate(dateStr: string): string {
    return new Date(dateStr).toLocaleDateString('tr-TR');
  }

  showToast(type: string, message: string): void {
    if (typeof window !== 'undefined' && (window as any).showToast) {
      (window as any).showToast(type, message);
    }
  }

  get wrongCount(): number {
    const q = this.newQuestion();
    return Math.max(0, q.solvedCount - q.correctCount);
  }

  updateSolvedCount(val: number): void {
    this.newQuestion.update(q => ({ ...q, solvedCount: val }));
  }

  updateCorrectCount(val: number): void {
    this.newQuestion.update(q => ({ ...q, correctCount: val }));
  }

  updateDurationMinutes(val: string): void {
    const parsed = val ? Math.round(parseFloat(val)) : undefined;
    this.newQuestion.update(q => ({ ...q, durationMinutes: parsed }));
  }

  // --- Edit Helpers ---
  updateEditSolvedCount(val: number): void {
    this.editQuestion.update((q: any) => ({ ...q, solvedCount: val }));
  }

  updateEditCorrectCount(val: number): void {
    this.editQuestion.update((q: any) => ({ ...q, correctCount: val }));
  }

  updateEditDurationMinutes(val: string): void {
    const parsed = val ? Math.round(parseFloat(val)) : null;
    this.editQuestion.update((q: any) => ({ ...q, durationMinutes: parsed }));
  }
}
