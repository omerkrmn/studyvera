using StudyVera.FrontEnd.Enums;
using StudyVera.FrontEnd.Models.Topics;

namespace StudyVera.FrontEnd.Models.UserLessonProgress
{
    public class UserLessonProgressDto
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public TopicDto? Topic { get; set; }
        public ProgressStatus ProgressStatus { get; set; }
    }
}
