using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.Auth.Commands;
using StudyVera.Domain.Entities; 
using StudyVera.Domain.Entities.Identity;

namespace StudyVera.Application.Handlers.Auth;

public class RegisterHandler : IRequestHandler<RegisterCommand, AppUser>
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AppUser> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ParameterNullException("parameters cannot be null!");

        var user = request.Adapt<AppUser>();

        user.UserSettings = new UserProfile
        {
            WeeklyQuestionGoal = 100, 
            CurrentTitle="Acemi",
            AllowFriendRequests = true,
            DailyReminderHour = 1,
            ShowRankInLeaderboard = true,
            DailyStudyMinuteGoal = 60,
            IsProfilePublic = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Language = "tr-TR",
            Theme = "Dark"
        };
        user.ProfileStat = new ProfileStat
        {
            UserId = user.Id,
            CurrentStreak = 0,
            BestStreak = 0,
            LastActivityDate = null
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return user;
        }
        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new Exception(errors);
    }
}