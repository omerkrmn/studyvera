export interface FriendDto {
  userName: string;
  score: number;
  title?: string;
}

export interface PendingUserDto {
  requestId: number;
  userName: string;
  sentAt: string; 
}