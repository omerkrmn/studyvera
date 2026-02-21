using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Dtos.Friendships;
using StudyVera.Application.Features.Friendships.Queries;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Friendships;

public class GetFriendsHandler : IRequestHandler<GetFriendScoresQuery, List<FriendDto>>
{
    private readonly IRepositoryManager _manager;
    private readonly UserManager<AppUser> _userManager;

    public GetFriendsHandler(IRepositoryManager manager, UserManager<AppUser> userManager)
    {
        _manager = manager;
        _userManager = userManager;
    }

    public async Task<List<FriendDto>> Handle(GetFriendScoresQuery request, CancellationToken cancellationToken)
    {
        var friendUsers = await _manager.FriendshipRepository.GetAllFriendsAsync(request.UserId, cancellationToken);
        if (friendUsers == null || !friendUsers.Any())
            return new List<FriendDto>();

        var response = friendUsers.Select(user => new FriendDto
        {
            UserName = user.UserName,
            Score = user.ProfileStat?.Score ?? 0,
        })
        .OrderByDescending(x => x.Score)
        .ToList();

        return response;
    }
}
