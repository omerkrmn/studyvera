using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class FriendshipRepository : RepositoryBase<Friendship>, IFriendshipRepository
{
    public FriendshipRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<AppUser>> GetAllFriendsAsync(Guid userId, CancellationToken ct)
    {
        return await _context.Friendships
            .Include(f => f.Requestor).
                ThenInclude(u => u.ProfileStat)
            .Include(f => f.Receiver).
                ThenInclude(u => u.ProfileStat)
            .Where(f => (f.RequestorId == userId || f.ReceiverId == userId) && f.Status == FriendshipStatus.Accepted)
            .Select(f => f.RequestorId == userId ? f.Receiver : f.Requestor)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<Friendship?> GetFriendshipBetweenUsersAsync(Guid user1Id, Guid user2Id, CancellationToken ct)
    {
        return await _context.Friendships
             .FirstOrDefaultAsync(f =>
                (f.RequestorId == user1Id && f.ReceiverId == user2Id) ||
                (f.RequestorId == user2Id && f.ReceiverId == user1Id), ct);
    }

    public async Task<List<Friendship>> GetPendingRequestsAsync(Guid userId, CancellationToken ct)
    {
        return await _context.Friendships
            .Include(f => f.Requestor)
            .Where(f => f.ReceiverId == userId && f.Status == FriendshipStatus.Pending)
            .ToListAsync(ct);
    }
}
