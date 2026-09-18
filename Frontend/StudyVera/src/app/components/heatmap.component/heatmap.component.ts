import { Component, OnInit, inject, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { firstValueFrom } from 'rxjs';
import { UserHistoryService } from '../../services/user-history.service';

interface DayActivity {
  date: Date;
  count: number;
  dayOfWeek: number;
}

@Component({
  selector: 'app-heatmap',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './heatmap.component.html',
  styleUrl: './heatmap.component.css',
})
export class HeatmapComponent implements OnInit {
  private historyService = inject(UserHistoryService);

  // --- State ---
  heatmapDays = signal<DayActivity[]>([]);
  isLoading = signal(true);

  ngOnInit() {
    this.loadChartData();
  }

  async loadChartData() {
    try {
      this.isLoading.set(true);
      const allDates = await firstValueFrom(this.historyService.getAllByDate()) as any[];

      // Group by date string (YYYY-MM-DD) for accuracy
      const grouped = allDates.reduce((acc, curr) => {
        const dateVal = curr.date ? curr.date : curr;
        const d = new Date(dateVal);
        const key = d.toISOString().split('T')[0];
        acc[key] = (acc[key] || 0) + 1;
        return acc;
      }, {} as Record<string, number>);

      const today = new Date();
      today.setHours(0, 0, 0, 0);

      // Go back to the Monday of 52 weeks ago
      const startDate = new Date(today);
      startDate.setDate(startDate.getDate() - 364);
      while (startDate.getDay() !== 1) { // 1 = Monday
        startDate.setDate(startDate.getDate() - 1);
      }

      const days: DayActivity[] = [];
      const current = new Date(startDate);
      const end = new Date(today);

      while (current <= end) {
        const key = current.toISOString().split('T')[0];
        days.push({
          date: new Date(current),
          count: grouped[key] || 0,
          dayOfWeek: (current.getDay() + 6) % 7 // Shift so Mon=0, Sun=6
        });
        current.setDate(current.getDate() + 1);
      }

      this.heatmapDays.set(days);
      this.isLoading.set(false);
    } catch (error) {
      console.error("Heatmap data load error:", error);
      this.isLoading.set(false);
    }
  }

  // --- Computed ---
  weeks = computed(() => {
    const days = this.heatmapDays();
    const result: DayActivity[][] = [];
    for (let i = 0; i < days.length; i += 7) {
      result.push(days.slice(i, i + 7));
    }
    return result;
  });

  gridWidth = computed(() => this.weeks().length * 14);

  monthLabels = computed(() => {
    const days = this.heatmapDays();
    const labels: { name: string; offset: number }[] = [];
    let lastMonth = -1;

    days.forEach((day, i) => {
      if (day.date.getDay() === 1) { // Only check on Mondays
        const currentMonth = day.date.getMonth();
        if (currentMonth !== lastMonth) {
          const weekIndex = Math.floor(i / 7);
          labels.push({
            name: day.date.toLocaleString('tr-TR', { month: 'short' }),
            offset: weekIndex * 14
          });
          lastMonth = currentMonth;
        }
      }
    });
    return labels;
  });

  // --- Helpers ---
  getLevel(count: number): number {
    if (count === 0) return 0;
    if (count <= 2) return 1;
    if (count <= 5) return 2;
    if (count <= 10) return 3;
    return 4;
  }

  formatDate(date: Date): string {
    return date.toLocaleDateString('tr-TR', { day: 'numeric', month: 'long', year: 'numeric' });
  }
}
