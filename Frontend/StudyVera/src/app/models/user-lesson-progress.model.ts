import { TopicDto } from './topic.model';

export enum ProgressStatus {
  InProgress = 0,
  Completed = 1
}

export interface UserLessonProgressDto {
  id: number;
  topicId: number;
  topic?: TopicDto; 
  progressStatus: ProgressStatus;
}

export interface AddUserLessonProgressDto {
  topicId: number;
  progressStatus: ProgressStatus;
}

export interface UpdateUserLessonProgressDto {
  progressStatus: ProgressStatus;
}