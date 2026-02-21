using StudyVera.Domain.Entities;
using StudyVera.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Interfaces;

public interface IFriendshipRepository : IRepository<Friendship>
{
    Task<Friendship?> GetFriendshipBetweenUsersAsync(Guid user1Id, Guid user2Id,CancellationToken ct);
    Task<List<Friendship>> GetPendingRequestsAsync(Guid userId,CancellationToken ct);
    Task<List<AppUser>> GetAllFriendsAsync(Guid userId,CancellationToken ct);
}