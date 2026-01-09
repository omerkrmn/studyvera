using StudyVera.Domain.Entities;

namespace StudyVera.Domain.Interfaces;

public interface IRepositoryManager
{
    IExamRepository ExamRepository { get; }
    ILessonRepository LessonRepository { get; }
    ITopicRepository TopicRepository { get; }
    IUserActivityHistoryRepository UserActivityHistoryRepository { get; }
    IUserLessonProgressRepository UserLessonProgressRepository { get; }
    IUserQuestionStatRepository UserQuestionStatRepository { get; }
    IProfileStatRepository ProfileStatRepository { get; }
    IUserProfileRepository UserProfileRepository{ get; }
    ILessonScheduleRepository LessonScheduleRepository { get; }
    IQuestionStatDetailRepository QuestionStatDetailRepository { get; }

    Task SaveChangesAsync(CancellationToken ct = default);
}
