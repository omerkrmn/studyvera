export interface LessonScheduleDto {
    lessonId: number;
    topicId: number;
    dayOfWeek: number;
    startTime: string;
    endTime: string;
    createdAt: string;
    lastUpdatedAt: string;
}

export interface AddLessonScheduleDto {
    lessonId: number;
    topicId: number;
    dayOfWeek: number;
    startTime: string;
    endTime: string;
}