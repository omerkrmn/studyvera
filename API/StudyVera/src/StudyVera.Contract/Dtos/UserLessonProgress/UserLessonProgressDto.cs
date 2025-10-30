using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Contract.Dtos.UserLessonProgress
{
    public class UserLessonProgressDto
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public TopicDto? Topic { get; set; }
        public ProgressStatus ProgressStatus { get; set; }
    }

}
