export interface ProfileViewModel {
  userName: string;
  email: string;
  targetExam: string;
  
  totalQuestions: number;
  userScore: number;
  globalRank: number;
  
  badgesEarned: number;
  currentStreak: number;
  
  goalRemainingQuestions: number;
  goalCompletionPercentage: number;
  
  activityData: Record<string, number>;
  
  deficiencyTopics: Record<string, number>;
}