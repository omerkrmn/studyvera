
export interface UserProfileDto {
  weeklyQuestionGoal: number;
  dailyStudyMinuteGoal: number;
  
  dailyReminderHour: number;
  isProfilePublic: boolean;
  showRankInLeaderboard: boolean;
  allowFriendRequests: boolean;
  
  theme: string;
  language: string;
  currentTitle: string;
  
  createdAt: string; 
  updatedAt: string;
}