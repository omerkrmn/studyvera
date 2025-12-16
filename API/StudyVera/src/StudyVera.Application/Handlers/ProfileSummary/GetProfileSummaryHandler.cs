using MediatR;
using StudyVera.Application.Dtos.ProfileSummary;
using StudyVera.Application.Features.ProfileSummary.Queries;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.ProfileSummary;

public class GetProfileSummaryHandler : IRequestHandler<GetProfileSummaryQuery, ProfileViewModel>
{
    private readonly IRepositoryManager _manager;

    public GetProfileSummaryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<ProfileViewModel> Handle(GetProfileSummaryQuery request, CancellationToken cancellationToken)
    {
        ProfileViewModel _model = new();
        (int TotalSolvedCount, int TotalCorrectCount) sumResult =
                    await _manager.UserQuestionStatRepository.GetSumAsync(request.UserId, cancellationToken);

        _model.TotalQuestions = sumResult.TotalSolvedCount;
        _model.UserScore = await _manager.ProfileStatRepository.GetScoreByUser(request.UserId, cancellationToken);
        _model.GlobalRank = await _manager.ProfileStatRepository.GetGlobalRankAsync(request.UserId, cancellationToken);
        
        return _model;
    }
}
