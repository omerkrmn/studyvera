
export interface UserActivityHistoryDto {
  id: number;
  activityDate: string;
  activityType: ActivityType | null;
  description: string;
}

export enum ActivityType {
  UserLogins = 0,
  LessonCompleted = 1,
  LessonProgressed = 2,
  SolvedAQuestion = 3,
  ProfileUpdated = 4,
  StudySessionCompleted = 5,
  TopicReviewed = 6,
  CreateMockExam = 7,
}