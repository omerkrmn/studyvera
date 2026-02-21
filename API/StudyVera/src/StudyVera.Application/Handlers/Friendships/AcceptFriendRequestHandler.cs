using MediatR;
using Microsoft.AspNetCore.Identity;
using StudyVera.Application.Features.Friendships.Commands;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Friendships;

public class AcceptFriendRequestHandler : IRequestHandler<AcceptFriendRequestCommand, bool>
{
    private readonly IRepositoryManager _manager;
    private readonly UserManager<AppUser> _userManager;

    public AcceptFriendRequestHandler(IRepositoryManager manager, UserManager<AppUser> userManager)
    {
        _manager = manager;
        _userManager = userManager;
    }

    public async Task<bool> Handle(AcceptFriendRequestCommand request, CancellationToken cancellationToken)
    {
        var senderUser = await _userManager.FindByNameAsync(request.From);
        if (senderUser == null)
            throw new Exception("İsteği gönderen kullanıcı bulunamadı.");

        var friendship = await _manager.FriendshipRepository.GetFriendshipBetweenUsersAsync(senderUser.Id, request.UserId, cancellationToken);

        if (friendship == null)
            throw new Exception("Böyle bir arkadaşlık isteği bulunamadı.");

        if (friendship.Status != FriendshipStatus.Pending)
            throw new Exception("Bu istek zaten sonuçlandırılmış.");

        if (friendship.ReceiverId != request.UserId)
            throw new Exception("Size ait olmayan bir arkadaşlık isteğini kabul edemezsiniz.");

        friendship.Status = FriendshipStatus.Accepted;
        friendship.ActionDate = DateTime.UtcNow;

        _manager.FriendshipRepository.Update(friendship);
        await _manager.SaveChangesAsync(cancellationToken);
        return true;
    }
}
