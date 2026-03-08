export interface MockExamDetailDto {
  lessonId: number;
  correctCount: number;
  wrongCount: number;
}

export interface CreateUserMockExamCommand {
  examId: number;
  publisher?: string;
  examName?: string;
  examDate: string; 
  details: MockExamDetailDto[];
}


export interface UserMockExamResponse {
  id: number;
  examName?: string;
  publisher?: string;
  examDate: string;
  totalNet: number;
  totalCorrect: number;
  totalWrong: number;
}

export interface MockDetailItemResponse {
  lessonName: string;
  correct: number;
  wrong: number;
  empty: number;
  net: number;
}

export interface UserMockExamDetailResponse {
  id: number;
  examName?: string;
  totalNet: number;
  details: MockDetailItemResponse[];
}