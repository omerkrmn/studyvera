using MediatR;
using StudyVera.Application.Dtos.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.Mocks.UserMockExamDetails.Queries;

public class GetUserMockExamDetailQuery : IRequest<UserMockExamDetailResponse>
{
    public int Id { get; set; }
}
