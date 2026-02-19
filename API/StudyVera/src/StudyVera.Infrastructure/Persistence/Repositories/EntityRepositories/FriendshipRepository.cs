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
    //Todo : Buradaki kodları refactor et. gereksiz select var gözüme kötü geliyor.
    public IQueryable<AppUser> GetFriendsWithScores(Guid userId)
    {
        return _context.Friendships
            .AsNoTracking()
            .Where(f => f.Status == FriendshipStatus.Accepted && (f.RequestorId == userId || f.ReceiverId == userId))
            .Select(f => f.RequestorId == userId ? f.Receiver : f.Requestor)
            .Include(u => u.ProfileStat); 
    }

    public IQueryable<AppUser> GetReceivedRequests(Guid userId)
    {
        return _context.Friendships
            .AsNoTracking()
            .Where(f => f.ReceiverId == userId && f.Status == FriendshipStatus.Pending)
            .Select(f => f.Requestor);
    }

    public IQueryable<AppUser> GetSentRequests(Guid userId)
    {
        return _context.Friendships
            .AsNoTracking()
            .Where(f => f.RequestorId == userId && f.Status == FriendshipStatus.Pending)
            .Select(f => f.Receiver);
    }
}
