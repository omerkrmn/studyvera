using MediatR;
using Microsoft.AspNetCore.Identity;
using StudyVera.Application.Features.Friendships.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Friendships;

public class SendFriendRequestHandler : IRequestHandler<SendFriendRequestCommand, bool>
{
    private readonly IRepositoryManager _manager;
    private readonly UserManager<AppUser> _userManager;

    public SendFriendRequestHandler(IRepositoryManager manager, UserManager<AppUser> userManager)
    {
        _manager = manager;
        _userManager = userManager;
    }

    public async Task<bool> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
    {
        var targetUser = await _userManager.FindByNameAsync(request.To);
        
        if (targetUser == null)
            throw new Exception("Hedef kullanıcı bulunamadı."); // hata kodu düzelt
        
        if (targetUser.Id == request.UserId)
            throw new Exception("Kendine arkadaşlık isteği gönderemezsin."); // hata kodu düzelt
        var existingFriendship = await _manager.FriendshipRepository.GetFriendshipBetweenUsersAsync(request.UserId, targetUser.Id, cancellationToken);
        
        if (existingFriendship != null)
        {
            if (existingFriendship.Status == FriendshipStatus.Accepted)
                throw new InvalidOperationException("Bu kullanıcıyla zaten arkadaşsınız.");

            if (existingFriendship.Status == FriendshipStatus.Pending)
                throw new InvalidOperationException("Bu kullanıcıya gönderilmiş bekleyen bir isteğin var.");

            if (existingFriendship.Status == FriendshipStatus.Blocked)
                throw new InvalidOperationException("Bu kullanıcıya şu an ulaşılamıyor.");
        }

        var friendship = new Friendship
        {
            RequestorId = request.UserId,
            ReceiverId = targetUser.Id,
            Status = FriendshipStatus.Pending,
            CreatedDate = DateTime.UtcNow
        };

        _manager.FriendshipRepository.Create(friendship);
        await _manager.SaveChangesAsync(cancellationToken);
        return true;
    }
}
