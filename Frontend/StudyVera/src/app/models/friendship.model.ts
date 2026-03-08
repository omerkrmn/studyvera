export interface FriendDto {
  userName: string;
  score: number;
}

export interface PendingUserDto {
  requestId: number;
  userName: string;
  sentAt: string; 
}