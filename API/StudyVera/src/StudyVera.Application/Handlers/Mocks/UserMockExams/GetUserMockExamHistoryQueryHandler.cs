using MediatR;
using StudyVera.Application.Dtos.Mocks;
using StudyVera.Application.Features.Mocks.UserMockExams.Queries;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Mocks.UserMockExams;

public class GetUserMockExamHistoryQueryHandler : IRequestHandler<GetUserMockExamHistoryQuery, List<UserMockExamResponse>>
{
    private readonly IRepositoryManager _repository;

    public GetUserMockExamHistoryQueryHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<List<UserMockExamResponse>> Handle(GetUserMockExamHistoryQuery request, CancellationToken cancellationToken)
    {
        var exams = await _repository.UserMockExamRepository.GetUserExamHistoryAsync(request.UserId, cancellationToken);

        return exams.Select(e => new UserMockExamResponse
        {
            Id = e.Id,
            ExamName = e.ExamName,
            Publisher = e.Publisher,
            ExamDate = e.ExamDate,
            TotalNet = e.TotalNet,
            TotalCorrect = e.TotalCorrect,
            TotalWrong = e.TotalWrong
        }).ToList();
    }
}