import { QuestionStatDetailDto } from './question-stat-detail.model';
import { TopicDto } from './topic.model';


export interface UserQuestionStatDto {
  id: number;
  
  topicId: number;
  topic: TopicDto;
  
  totalSolvedCount: number;
  totalCorrectCount: number;
  
  totalWrongCount: number;
  accuracyRate: number; 
  totalTimeSpentInMinutes: number;
  
  questionStatDetail: QuestionStatDetailDto[];
  
  lastAttemptAt: string; 
}

export interface AddUserQuestionStatDto {
  topicId: number;
  solvedCount: number;
  correctCount: number;
  durationMinutes?: number;
}