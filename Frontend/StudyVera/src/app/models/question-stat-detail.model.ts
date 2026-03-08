export interface QuestionStatDetailDto {
  id: number;
  solvedCount: number;
  correctCount: number;
  
  wrongCount: number; 
  
  attemptedAt: string; 
}