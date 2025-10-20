using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    Task SaveChangesAsync(CancellationToken ct = default);
}
