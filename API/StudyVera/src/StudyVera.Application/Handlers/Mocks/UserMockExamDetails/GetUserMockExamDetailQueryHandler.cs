using MediatR;
using StudyVera.Application.Dtos.Mocks;
using StudyVera.Application.Features.Mocks.UserMockExamDetails.Queries;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Mocks.UserMockExamDetails;

public class GetUserMockExamDetailQueryHandler : IRequestHandler<GetUserMockExamDetailQuery, UserMockExamDetailResponse>
{
    private readonly IRepositoryManager _repository;

    public GetUserMockExamDetailQueryHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<UserMockExamDetailResponse> Handle(GetUserMockExamDetailQuery request, CancellationToken cancellationToken)
    {
        var exam = await _repository.UserMockExamRepository.GetWithDetailsAsync(request.Id, cancellationToken); // ekle
        if (exam == null) return null!;

        return new UserMockExamDetailResponse
        {
            Id = exam.Id,
            ExamName = exam.ExamName,
            TotalNet = exam.TotalNet,
            Details = exam.Details.Select(d => new MockDetailItemResponse
            {
                LessonName = d.Lesson.Name,
                Correct = d.CorrectCount,
                Wrong = d.WrongCount,
                Empty = d.EmptyCount,
                Net = d.Net
            }).ToList()
        };
    }
}