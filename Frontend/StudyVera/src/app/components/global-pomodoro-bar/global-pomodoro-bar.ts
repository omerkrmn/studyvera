import { Component, computed, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, NavigationEnd } from '@angular/router';
import { PomodoroService } from '../../services/pomodoro.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-global-pomodoro-bar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './global-pomodoro-bar.html',
  styleUrl: './global-pomodoro-bar.css'
})
export class GlobalPomodoroBar {
  pomodoroService = inject(PomodoroService);
  private router = inject(Router);

  private currentUrl = toSignal(
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      map(() => this.router.url)
    ),
    { initialValue: this.router.url }
  );

  isPomodoroPage = computed(() => {
    return this.currentUrl().split('?')[0] === '/pomodoro';
  });

  isActive = computed(() => {
    // Show bar if running, if in break, or if time has been spent but not reset
    const service = this.pomodoroService;
    return service.isRunning() || 
           service.isBreakMode() || 
           service.isOverdue() ||
           service.remainingSeconds() !== service.currentMode() * 60;
  });

  isVisible = computed(() => {
    return this.isActive() && !this.isPomodoroPage();
  });

  timeDisplay = computed(() => {
    const absSeconds = Math.abs(this.pomodoroService.remainingSeconds());
    const mins = Math.floor(absSeconds / 60);
    const secs = absSeconds % 60;
    const sign = this.pomodoroService.remainingSeconds() < 0 ? "-" : "";
    return `${sign}${mins.toString().padStart(2, '0')}:${secs.toString().padStart(2, '0')}`;
  });

  progress = computed(() => {
    if (this.pomodoroService.isBreakMode()) {
      if (this.pomodoroService.isOverdue()) {
        return 100;
      }
      const total = this.pomodoroService.getMolaSuresiDakika(this.pomodoroService.currentMode()) * 60;
      const remaining = this.pomodoroService.remainingSeconds();
      return total > 0 ? Math.max(0, Math.min(100, ((total - remaining) / total) * 100)) : 0;
    } else {
      const total = this.pomodoroService.currentMode() * 60;
      const remaining = this.pomodoroService.remainingSeconds();
      return total > 0 ? Math.max(0, Math.min(100, ((total - remaining) / total) * 100)) : 0;
    }
  });

  toggleTimer() {
    if (this.pomodoroService.isRunning()) {
      this.pomodoroService.stopTimer();
    } else {
      if (this.pomodoroService.isBreakMode()) {
        this.pomodoroService.startBreak();
      } else {
        this.pomodoroService.startTimer();
      }
    }
  }

  resetTimer() {
    this.pomodoroService.resetTimer();
  }
}
