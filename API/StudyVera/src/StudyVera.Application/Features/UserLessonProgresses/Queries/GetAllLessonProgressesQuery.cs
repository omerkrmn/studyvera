using MediatR;
using StudyVera.Contract.Dtos.UserLessonProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.UserLessonProgresses.Queries
{
    public class GetAllLessonProgressesQuery : IRequest<List<UserLessonProgressDto>>
    {
        public Guid UserId { get; set; }
    }

}
