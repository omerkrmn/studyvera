
export interface UserWeeklyGoalDto {
  id?: number;
  targetQuestionCount: number;
  solvedQuestionCount: number;
  targetStudyHours: number;
  completedStudyHours: number;
  startDate: string; 
  endDate: string;
  isCompleted: boolean;
}