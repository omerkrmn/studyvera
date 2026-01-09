using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Models;
using StudyVera.Application.Dtos.ProfileStats;
using StudyVera.Application.Features.ProfileStats.Queries;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.ProfileStats;

public class GetScoreBoardHander : IRequestHandler<GetScoreBoardQuery, PagedList<ScoreBoardDto>>
{
    private readonly IRepositoryManager _manager;

    public GetScoreBoardHander(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<PagedList<ScoreBoardDto>> Handle(GetScoreBoardQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = _manager.ProfileStatRepository
                                .FindAll(false)
                                .OrderByDescending(a => a.Score);

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var scoreList = await baseQuery
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(a => new ScoreBoardDto
                            {
                                NickName = a.User.UserName,
                                Score = a.Score
                            })
                            .ToListAsync(cancellationToken);

        return new PagedList<ScoreBoardDto>(scoreList, totalCount, request.PageNumber, request.PageSize);
    }
}
