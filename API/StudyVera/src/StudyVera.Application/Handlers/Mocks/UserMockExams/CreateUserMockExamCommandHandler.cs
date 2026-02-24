using MediatR;
using StudyVera.Application.Features.Mock.UserMockExams.Commands;
using StudyVera.Domain.Entities.Mock;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Mocks.UserMockExams;

public class CreateUserMockExamCommandHandler : IRequestHandler<CreateUserMockExamCommand, int>
{
    private readonly IRepositoryManager _manager;

    public CreateUserMockExamCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<int> Handle(CreateUserMockExamCommand request, CancellationToken cancellationToken)
    {
        var mockExam = new UserMockExam
        {
            UserId = request.UserId,
            ExamId = request.ExamId,
            Publisher = request.Publisher,
            ExamName = request.ExamName,
            ExamDate = request.ExamDate,
            Details = new List<UserMockExamDetail>()
        };

        foreach (var item in request.Details)
        {
            var lesson = _manager.LessonRepository.FindByCondition(l=>l.Id == item.LessonId,false).SingleOrDefault();

            if (lesson == null) 
                continue;

            var detail = new UserMockExamDetail
            {
                LessonId = item.LessonId,
                CorrectCount = item.CorrectCount,
                WrongCount = item.WrongCount,
                EmptyCount = lesson.ExamQuestionCount - (item.CorrectCount + item.WrongCount)
            };

            mockExam.Details.Add(detail);
        }

        mockExam.TotalCorrect = mockExam.Details.Sum(x => x.CorrectCount);
        mockExam.TotalWrong = mockExam.Details.Sum(x => x.WrongCount);
        mockExam.TotalEmpty = mockExam.Details.Sum(x => x.EmptyCount);

        mockExam.TotalNet = mockExam.Details.Sum(x => x.Net);

        _manager.UserMockExamRepository.Create(mockExam);

        await _manager.SaveChangesAsync();

        return mockExam.Id;
    }
}
