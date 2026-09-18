
export interface UserWeeklyGoalDto {
  weekStartDate: string;
  targetQuestionCount: number;
  targetStudyMinutes: number;
  currentQuestionCount: number;
  currentStudyMinutes: number;
  remainingQuestions: number;
  remainingStudyMinutes: number;
  completionPercentage: number;
  studyCompletionPercentage: number;
  isGoalAchieved: boolean;
  isStudyGoalAchieved: boolean;
  statusMessage: string;
}