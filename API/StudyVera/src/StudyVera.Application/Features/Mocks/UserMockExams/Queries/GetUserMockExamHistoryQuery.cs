using MediatR;
using StudyVera.Application.Dtos.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.Mocks.UserMockExams.Queries;

public class GetUserMockExamHistoryQuery : IRequest<List<UserMockExamResponse>>
{
    public Guid UserId { get; set; }
}