namespace StudyVera.FrontEnd.Models.Friendships;

public class PendingUserDto
{
    public int RequestId { get; init; }
    public string UserName { get; init; }

    public DateTime SentAt { get; init; }
}