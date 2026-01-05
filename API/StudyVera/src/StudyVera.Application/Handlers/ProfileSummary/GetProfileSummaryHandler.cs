using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Dtos.ProfileSummary;
using StudyVera.Application.Features.ProfileSummary.Queries;
using StudyVera.Domain.Interfaces;
using StudyVera.Domain.Interfaces.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.ProfileSummary;

public class GetProfileSummaryHandler : IRequestHandler<GetProfileSummaryQuery, ProfileViewModel>
{
    private readonly IRepositoryManager _manager;
    private readonly ITopicDeficiencyService _topicDeficiencyService;
    public GetProfileSummaryHandler(IRepositoryManager manager, ITopicDeficiencyService topicDeficiencyService)
    {
        _manager = manager;
        _topicDeficiencyService = topicDeficiencyService;
    }

    public async Task<ProfileViewModel> Handle(GetProfileSummaryQuery request, CancellationToken cancellationToken)
    {
        ProfileViewModel _model = new();

        (int TotalSolvedCount, int TotalCorrectCount) sumResult = await _manager.UserQuestionStatRepository.GetSumAsync(request.UserId, cancellationToken);

        _model.TotalQuestions = sumResult.TotalSolvedCount;
        _model.UserScore = await _manager.ProfileStatRepository.GetScoreByUserAsync(request.UserId, cancellationToken);
        _model.GlobalRank = await _manager.ProfileStatRepository.GetGlobalRankAsync(request.UserId, cancellationToken);

        var statsFromDb = await _manager.UserQuestionStatRepository
            .FindByCondition(uqs => uqs.UserId == request.UserId, false)
            .Where(s => s.TotalSolvedCount > 50)
            .Select(uqs => new
            {
                TopicName = uqs.Topic.Name,
                uqs.TotalSolvedCount,
                uqs.TotalCorrectCount,
                uqs.LastAttemptAt,
                TopicPriority = uqs.Topic.Priority
            })
            .ToListAsync(cancellationToken);

        _model.DeficiencyTopics = statsFromDb
            .Select(s => new
            {
                s.TopicName,
                DeficiencyScore = _topicDeficiencyService.CalculateDeficiencyScore(
                    s.TotalSolvedCount,
                    s.TotalCorrectCount,
                    s.LastAttemptAt,
                    s.TopicPriority)
            })
            .OrderByDescending(x => x.DeficiencyScore) 
            .Take(3) 
            .ToDictionary(x => x.TopicName, x => (int)x.DeficiencyScore);

        return _model;
    }
}
