using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.Auth.Commands;
using StudyVera.Domain.Entities; 
using StudyVera.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace StudyVera.Application.Handlers.Auth;

public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var user = request.Adapt<AppUser>();

        if (string.IsNullOrEmpty(user.UserName)) user.UserName = user.Email;

        user.UserSettings = new UserProfile
        {
            WeeklyQuestionGoal = 100,
            CurrentTitle = "Acemi",
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
            CurrentStreak = 0,
            BestStreak = 0,
            LastActivityDate = null
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ValidationException(errors);
        }

        return true;
    }
}