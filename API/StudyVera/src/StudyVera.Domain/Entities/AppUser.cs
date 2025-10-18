using Microsoft.AspNetCore.Identity;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public ExamTarget? TargetExam { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserLessonProgress> LessonProgresses { get; set; } = [];
        public ICollection<UserQuestionStat> QuestionStats { get; set; } = [];
    }
}
