using StudyVera.Domain.Interfaces;
using StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

namespace StudyVera.Infrastructure.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        #region Lazy Implementations
        private readonly Lazy<IExamRepository> _examRepository;
        private readonly Lazy<ILessonRepository> _lessonRepository;
        private readonly Lazy<ITopicRepository> _topicRepository;
        private readonly Lazy<IUserActivityHistoryRepository> _userActivityHistoryRepository;
        private readonly Lazy<IUserLessonProgressRepository> _userLessonProgressRepository;
        private readonly Lazy<IUserQuestionStatRepository> _userQuestionStatRepository;
        private readonly Lazy<IProfileStatRepository> _profileStatRepository;
        private readonly Lazy<IUserSettingsRepository> _userSettingsRepository;
        private readonly Lazy<ILessonScheduleRepository> _lessonScheduleRepository;
        private readonly Lazy<IQuestionStatDetailRepository> _questionStatDetailRepository;
        #endregion
        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            _examRepository = new Lazy<IExamRepository>(() => new ExamRepository(_context));
            _lessonRepository = new Lazy<ILessonRepository>(() => new LessonRepository(_context));
            _topicRepository = new Lazy<ITopicRepository>(() => new TopicRepository(_context));
            _userActivityHistoryRepository = new Lazy<IUserActivityHistoryRepository>(() => new UserActivityHistoryRepository(_context));
            _userLessonProgressRepository = new Lazy<IUserLessonProgressRepository>(() => new UserLessonProgressRepository(_context));
            _userQuestionStatRepository = new Lazy<IUserQuestionStatRepository>(() => new UserQuestionStatRepository(_context));
            _profileStatRepository = new Lazy<IProfileStatRepository>(() => new ProfileStatRepository(_context));
            _userSettingsRepository = new Lazy<IUserSettingsRepository>(() => new UserSettingsRepository(_context));
            _lessonScheduleRepository = new Lazy<ILessonScheduleRepository>(() => new LessonScheduleRepository(_context));
            _questionStatDetailRepository = new Lazy<IQuestionStatDetailRepository>(() => new QuestionStatDetailRepository(_context));
        }

        #region Repositories
        public IExamRepository ExamRepository => _examRepository.Value;
        public ILessonRepository LessonRepository => _lessonRepository.Value;
        public ITopicRepository TopicRepository => _topicRepository.Value;
        public IUserActivityHistoryRepository UserActivityHistoryRepository => _userActivityHistoryRepository.Value;
        public IUserLessonProgressRepository UserLessonProgressRepository => _userLessonProgressRepository.Value;
        public IUserQuestionStatRepository UserQuestionStatRepository => _userQuestionStatRepository.Value;
        public IProfileStatRepository ProfileStatRepository => _profileStatRepository.Value;
        public IUserSettingsRepository UserSettingsRepository => _userSettingsRepository.Value;
        public ILessonScheduleRepository LessonScheduleRepository => _lessonScheduleRepository.Value;
        public IQuestionStatDetailRepository QuestionStatDetailRepository => _questionStatDetailRepository.Value;
        #endregion
        #region SaveChanges
        public Task SaveChangesAsync(CancellationToken ct = default) => _context.SaveChangesAsync(ct);
        #endregion
    }

}
