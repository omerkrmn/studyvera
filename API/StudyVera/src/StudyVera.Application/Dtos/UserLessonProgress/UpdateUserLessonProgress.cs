using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos.UserLessonProgress
{
    public class UpdateUserLessonProgress
    {
        public ProgressStatus ProgressStatus { get; set; }
    }

}
