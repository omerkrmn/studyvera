using StudyVera.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Contract.Services;

public interface IServiceManager
{
    IUserLessonProgressManager UserLessonProgressManager { get; }
    IAuthenticationManager AuthenticationManager { get; }
    IUserActivityHistoryManager UserActivityHistoryManager { get; }
    IUserQuestionStatManager UserQuestionStatManager { get; }
    IProfileStatManager ProfileStatManager { get; }

}
