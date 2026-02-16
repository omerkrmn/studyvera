using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities
{
    public class UserWeeklyGoal
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }

        public DateTime WeekStartDate { get; set; }

        public int TargetQuestionCount { get; set; }
        public int TargetStudyMinutes { get; set; }

        public int CurrentQuestionCount { get; set; }
        public int CurrentStudyMinutes { get; set; }

        public bool IsCompleted => CurrentQuestionCount >= TargetQuestionCount;
        public static DateTime GetCurrentWeekStartDate()
        {
            DateTime now = DateTime.UtcNow;

            int diff = (7 + (now.DayOfWeek - DayOfWeek.Monday)) % 7;

            return now.AddDays(-1 * diff).Date;
        }
    }

}
