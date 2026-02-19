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
    IQueryable<AppUser> GetFriendsWithScores(Guid userId);

    IQueryable<AppUser> GetReceivedRequests(Guid userId);

    IQueryable<AppUser> GetSentRequests(Guid userId);
}