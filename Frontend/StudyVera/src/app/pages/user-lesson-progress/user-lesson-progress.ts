

import { Component, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserLessonProgressService } from '../../services/user-lesson-progress.service';
import { TopicService } from '../../services/topic.service';
import { LessonService } from '../../services/lesson.service';
import { UserLessonProgressDto, ProgressStatus } from '../../models/user-lesson-progress.model';
import { TopicDto } from '../../models/topic.model';
import { LessonDto } from '../../models/lesson.model';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-progress',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-lesson-progress.html',
  styleUrls: ['./user-lesson-progress.css']
})
export class UserLessonProgress implements OnInit {
  progresses = signal<UserLessonProgressDto[]>([]);
  topics = signal<TopicDto[]>([]);
  lessons = signal<LessonDto[]>([]);
  isLoading = signal<boolean>(true);

  searchText = signal<string>('');
  filterStatus = signal<string>('All');
  activeLessonIds = signal<Set<number>>(new Set());

  // İstatistikler için computed sinyaller
  totalTopicsCount = computed(() => this.topics().length);
  completedCount = computed(() => 
    this.progresses().filter(p => Number(p.progressStatus) === ProgressStatus.Completed).length
  );
  inProgressCount = computed(() => 
    this.progresses().filter(p => Number(p.progressStatus) === ProgressStatus.InProgress).length
  );
  overallProgress = computed(() => {
    const total = this.totalTopicsCount();
    if (total === 0) return 0;
    return Math.round((this.completedCount() / total) * 100);
  });

  constructor(
    private progressService: UserLessonProgressService,
    private topicService: TopicService,
    private lessonService: LessonService
  ) { }

  async ngOnInit() {
    this.loadData(true); // İlk yüklemede spinner göster
  }
  onSearch(event: Event) {
    const input = event.target as HTMLInputElement;
    this.searchText.set(input.value);
  }
  onFilterChange(event: Event) {
    const select = event.target as HTMLSelectElement;
    this.filterStatus.set(select.value);
  }

  getKeys(obj: any): string[] {
    return Object.keys(obj);
  }

  getLessonName(lessonId: number): string {
    const lesson = this.lessons().find(l => l.id === lessonId);
    return lesson ? lesson.name : 'Ders';
  }
  async loadData(showLoading: boolean = false) {
    if (showLoading) this.isLoading.set(true);
    try {
      const [p, t, l] = await Promise.all([
        firstValueFrom(this.progressService.getAll()),
        firstValueFrom(this.topicService.getTopics()),
        firstValueFrom(this.lessonService.getAll())
      ]);

      this.progresses.set(p || []);
      this.topics.set(t || []);
      this.lessons.set(l || []);
    } catch (error) {
      console.error('Veri yükleme hatası:', error);
    } finally {
      if (showLoading) this.isLoading.set(false);
    }
  }

  getLessonStats(lessonId: number) {
    const lessonTopics = this.topics().filter(t => t.lessonId === lessonId);
    const total = lessonTopics.length;
    const completed = lessonTopics.filter(t => 
      this.progresses().some(p => Number(p.topicId) === t.id && Number(p.progressStatus) === ProgressStatus.Completed)
    ).length;
    const percent = total === 0 ? 0 : Math.round((completed / total) * 100);
    return { total, completed, percent };
  }

  getTopicStatus(topicId: number): string {
    const topicProgresses = this.progresses().filter(p => Number(p.topicId) === Number(topicId));
    
    if (topicProgresses.some(p => Number(p.progressStatus) === ProgressStatus.Completed)) {
      return 'Completed';
    }
    if (topicProgresses.some(p => Number(p.progressStatus) === ProgressStatus.InProgress)) {
      return 'InProgress';
    }
    return 'NotStarted';
  }

  filteredGroups = computed(() => {
    const allTopics = this.topics();
    const search = this.searchText().toLowerCase();
    const statusFilter = this.filterStatus();

    return allTopics
      .filter(t => {
        const matchesSearch = t.name.toLowerCase().includes(search);
        if (!matchesSearch) return false;

        if (statusFilter === 'All') return true;
        
        const status = this.getTopicStatus(t.id);
        return status === statusFilter;
      })
      .sort((a, b) => (a.priority || 0) - (b.priority || 0))
      .reduce((acc, topic) => {
        const key = topic.lessonId;
        if (!acc[key]) acc[key] = [];
        acc[key].push(topic);
        return acc;
      }, {} as Record<number, TopicDto[]>);
  });

  toggleLesson(lessonId: number) {
    const current = new Set(this.activeLessonIds());
    if (current.has(lessonId)) current.delete(lessonId);
    else current.add(lessonId);
    this.activeLessonIds.set(current);
  }

  toggleAllLessons() {
    const allLessonIds = Array.from(new Set(this.topics().map(t => t.lessonId)));
    if (this.activeLessonIds().size === allLessonIds.length) {
      this.activeLessonIds.set(new Set());
    } else {
      this.activeLessonIds.set(new Set(allLessonIds));
    }
  }

  async startTopic(topicId: number) {
    try {
      await firstValueFrom(this.progressService.add({ topicId, progressStatus: ProgressStatus.InProgress }));
      await this.loadData(false);
      (window as any).showToast('success', 'İyi çalışmalar! Konu durumu "Devam Ediyor" olarak güncellendi.');
    } catch (error) {
      console.error('Konu başlatma hatası:', error);
      (window as any).showToast('error', 'Konu başlatılırken bir hata oluştu.');
    }
  }

  async markAsCompleted(topicId: number) {
    const progress = this.progresses().find(p => Number(p.topicId) === topicId);
    if (progress) {
      try {
        await firstValueFrom(this.progressService.update(progress.id));
        await this.loadData(false); // Sessiz güncelleme
        (window as any).showToast('success', 'Tebrikler! Konu başarıyla tamamlandı.');
      } catch (error) {
        console.error('Konu tamamlama hatası:', error);
        (window as any).showToast('error', 'Konu güncellenirken bir hata oluştu.');
      }
    }
  }

  suggestRandomTopic() {
    const notStarted = this.topics().filter(t => !this.progresses().some(p => Number(p.topicId) === t.id));
    if (notStarted.length > 0) {
      const randomTopic = notStarted[Math.floor(Math.random() * notStarted.length)];
      const lesson = this.lessons().find(l => l.id === randomTopic.lessonId);
      alert(`🎯 Önerilen Çalışma:\n\nDers: ${lesson?.name}\nKonu: ${randomTopic.name}\n\nHemen başlamaya ne dersin?`);
    } else {
      alert("Harika! Tüm konulara en azından başlamışsın.");
    }
  }
}