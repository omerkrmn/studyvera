using StudyVera.FrontEnd.Models.UserProfile;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface IUserProfileService
{
    public Task<UserProfileDto?> GetProfileAsync();
    public Task UpdateProfileAsync(UserProfileDto dto);

}
