export interface QuestionStatDetailDto {
  id: number;
  solvedCount: number;
  correctCount: number;
  
  wrongCount: number; 
  durationMinutes?: number;
  
  attemptedAt: string; 
}