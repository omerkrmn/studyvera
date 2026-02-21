using StudyVera.FrontEnd.Models.Friendships;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface IFriendshipService
{
    Task<List<FriendDto>> GetAllFriends();
    Task AddFriend(string friendUserName);
    Task AcceptFriend(string friendUserName);
    Task<List<PendingUserDto>> GetReceivedRequests();
}