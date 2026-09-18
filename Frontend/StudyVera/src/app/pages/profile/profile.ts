import { Component, OnInit, inject, signal, computed, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { forkJoin } from 'rxjs';

import { ProfileSummaryService } from '../../services/profile-summary.service';
import { UserWeeklyGoalService } from '../../services/user-weekly-goal.service';
import { ScoreBoardComponent } from '../../components/score.board.component/score.board.component';
import { HeatmapComponent } from '../../components/heatmap.component/heatmap.component';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    ScoreBoardComponent,
    HeatmapComponent
  ],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements OnInit {
  isLoading = signal<boolean>(true);
  isDataLoaded = signal<boolean>(false);
  errorMessage = signal<string | null>(null);
  profileData = signal<any>(null);
  weeklyGoal = signal<any>(null);
  isLeaderboardVisible = signal<boolean>(true);
  isBadgesVisible = signal<boolean>(true);

  constructor() {
    if (typeof window !== 'undefined') {
      const savedLeaderboard = localStorage.getItem('sv_leaderboard_visible');
      if (savedLeaderboard !== null) {
        this.isLeaderboardVisible.set(savedLeaderboard === 'true');
      }

      const savedBadges = localStorage.getItem('sv_badges_visible');
      if (savedBadges !== null) {
        this.isBadgesVisible.set(savedBadges === 'true');
      }
    }

    effect(() => {
      if (typeof window !== 'undefined') {
        localStorage.setItem('sv_leaderboard_visible', String(this.isLeaderboardVisible()));
        localStorage.setItem('sv_badges_visible', String(this.isBadgesVisible()));
      }
    });
  }

  private titleService = inject(Title);
  private profileSummaryService = inject(ProfileSummaryService);
  private userWeeklyGoalService = inject(UserWeeklyGoalService);

  ngOnInit() {
    this.titleService.setTitle('StudyVera - Profil ve Performans Analizi');
    this.loadProfileData();
  }

  loadProfileData() {
    this.isLoading.set(true);
    this.errorMessage.set(null);

    forkJoin({
      profile: this.profileSummaryService.getProfileSummary(),
      weeklyGoal: this.userWeeklyGoalService.getUserWeeklyGoal()
    }).subscribe({
      next: (res) => {
        this.profileData.set(res.profile);

        const goal = res.weeklyGoal;
        if (goal) {
          goal.remainingStudyMinutes = Math.max(0, goal.targetStudyMinutes - goal.currentStudyMinutes);
          goal.studyCompletionPercentage = goal.targetStudyMinutes > 0 ?
            Math.min(100, Math.round((goal.currentStudyMinutes / goal.targetStudyMinutes) * 1000) / 10) : 0;
          goal.isStudyGoalAchieved = goal.currentStudyMinutes >= goal.targetStudyMinutes;
        }
        this.weeklyGoal.set(goal);

        this.isDataLoaded.set(true);
        this.isLoading.set(false);
      },
      error: (err) => {
        const errorMsg = err.error?.Message || err.error?.message || 'Veri yüklenirken bir hata oluştu.';
        this.errorMessage.set(errorMsg);
        this.isLoading.set(false);

        if (typeof window !== 'undefined' && (window as any).showToast) {
          (window as any).showToast('error', errorMsg);
        }
      }
    });
  }

  // --- Computed Signals ---

  level = computed(() => {
    const data = this.profileData();
    return data ? Math.floor(data.userScore / 1000) + 1 : 1;
  });

  nextLevelRemaining = computed(() => {
    const data = this.profileData();
    return data ? 1000 - (data.userScore % 1000) : 1000;
  });

  levelProgress = computed(() => {
    const data = this.profileData();
    return data ? Math.floor((data.userScore % 1000) / 10.0) : 0;
  });

  priorityDeficiencies = computed(() => {
    const data = this.profileData();
    if (!data?.deficiencyTopics) return [];
    return Object.entries(data.deficiencyTopics)
      .map(([key, value]) => ({ key, value: value as number }))
      .filter(topic => topic.value > 100);
  });

  /** PFD R5: İlk deficiency topic'i "önerilen çalışma" olarak sunulur */
  suggestedTopic = computed(() => {
    const deficiencies = this.priorityDeficiencies();
    if (deficiencies.length > 0) {
      return { name: deficiencies[0].key, hasDeficiency: true };
    }
    return { name: null, hasDeficiency: false };
  });

  /** PFD R5: Hero section'da hedef özetini göstermek için */
  questionGoalProgress = computed(() => {
    const goal = this.weeklyGoal();
    return goal?.completionPercentage ?? 0;
  });

  studyGoalProgress = computed(() => {
    const goal = this.weeklyGoal();
    return goal?.studyCompletionPercentage ?? 0;
  });
}
