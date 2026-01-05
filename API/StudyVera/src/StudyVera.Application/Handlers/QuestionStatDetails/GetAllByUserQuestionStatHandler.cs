using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Dtos.QuestionStatDetails;
using StudyVera.Application.Features.QuestionStatDetails.Queries;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.QuestionStatDetails;

public class GetAllByUserQuestionStatHandler : IRequestHandler<GetAllByUserQuestionStatQuery, List<QuestionStatDetailDto>>
{
    private readonly IRepositoryManager _manager;

    public GetAllByUserQuestionStatHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<List<QuestionStatDetailDto>> Handle(GetAllByUserQuestionStatQuery request, CancellationToken cancellationToken)
    {

        
        var allQuestionStatDetails =  await _manager
                                            .QuestionStatDetailRepository
                                            .FindByCondition(qsd => qsd.UserQuestionStatId == request.QuestionStatId, false)
                                            .Select(qsd => new QuestionStatDetailDto
                                            {
                                                AttemptedAt=qsd.AttemptedAt,
                                                CorrectCount=qsd.CorrectCount,
                                                SolvedCount = qsd.SolvedCount
                                            })
                                            .ToListAsync(cancellationToken);

        return allQuestionStatDetails;

    }
}
