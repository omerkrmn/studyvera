import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProfileStatService } from '../../services/profile-stat.service';
import { FriendshipService } from '../../services/friendship.service';
import { ScoreBoardDto } from '../../models/profile-stat.model';
import { PendingUserDto } from '../../models/friendship.model';

@Component({
  selector: 'app-score',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './score.board.component.html',
  styleUrl: './score.board.component.css',
})
export class ScoreBoardComponent implements OnInit {
  private profileStatService = inject(ProfileStatService);
  private friendshipService = inject(FriendshipService);

  scoreBoardList = signal<ScoreBoardDto[] | null>(null);
  pendingRequests = signal<PendingUserDto[]>([]);
  showOnlyFriends = signal<boolean>(false);
  targetFriendName = signal<string>('');
  isRequestSending = signal<boolean>(false);

  ngOnInit() {
    this.loadData();
    this.loadPendingRequests();
  }

  loadData() {
    this.scoreBoardList.set(null);
    
    if (this.showOnlyFriends()) {
      this.friendshipService.getAllFriends().subscribe({
        next: (friends) => {
          const mapped = friends.map(f => ({
            nickName: f.userName, 
            score: f.score,
            title: f.title
          } as unknown as ScoreBoardDto)).sort((a, b) => b.score - a.score);
          this.scoreBoardList.set(mapped);
        },
        error: () => this.scoreBoardList.set([])
      });
    } else {
      this.profileStatService.getScoreBoard(1, 4).subscribe({
        next: (results) => {
          const sorted = results.sort((a, b) => b.score - a.score);
          this.scoreBoardList.set(sorted);
        },
        error: () => this.scoreBoardList.set([])
      });
    }
  }

  loadPendingRequests() {
    this.friendshipService.getReceivedRequests().subscribe({
      next: (reqs) => this.pendingRequests.set(reqs || []),
      error: () => this.pendingRequests.set([])
    });
  }

  toggleLeaderboard(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.showOnlyFriends.set(isChecked);
    this.loadData();
  }

  sendFriendRequest() {
    const friendName = this.targetFriendName().trim();
    if (!friendName) return;

    this.isRequestSending.set(true);
    this.friendshipService.addFriend(friendName).subscribe({
      next: () => {
        this.targetFriendName.set("");
        this.isRequestSending.set(false);
        
        if (typeof window !== 'undefined' && (window as any).showToast) {
          (window as any).showToast('success', 'İstek başarıyla gönderildi!');
        }
      },
      error: (err) => {
        const errorMsg = err.error?.Message || err.error?.message || 'İstek gönderilemedi';
        this.isRequestSending.set(false);
        
        if (typeof window !== 'undefined' && (window as any).showToast) {
          (window as any).showToast('error', errorMsg);
        }
      }
    });
  }

  acceptRequest(userName: string) {
    this.friendshipService.acceptFriend(userName).subscribe({
      next: () => {
        this.loadData();
        this.loadPendingRequests();
        if (typeof window !== 'undefined' && (window as any).showToast) {
          (window as any).showToast('success', 'Arkadaşlık isteği onaylandı!');
        }
      },
      error: (err) => {
        const errorMsg = err.error?.Message || err.error?.message || 'İstek onaylanırken hata oluştu';

        if (typeof window !== 'undefined' && (window as any).showToast) {
          (window as any).showToast('error', errorMsg);
        }
      }
    });
  }

  getRankClass(index: number): string {
    switch (index) {
      case 0: return "rank-gold";
      case 1: return "rank-silver";
      case 2: return "rank-bronze";
      default: return "rank-normal";
    }
  }
}
