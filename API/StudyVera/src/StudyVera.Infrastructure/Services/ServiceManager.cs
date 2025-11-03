using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using StudyVera.Contract.Interfaces;
using StudyVera.Contract.Services;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Interfaces;
using StudyVera.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserLessonProgressManager> _userLessonProgressManager;
    private readonly Lazy<IAuthenticationManager> _authenticationManager;
    private readonly Lazy<IUserActivityHistoryManager> _userActivityHistoryManager;
    private readonly Lazy<IUserQuestionStatManager> _userQuestionStatManager;
    private readonly Lazy<IProfileStatManager> _profileStatManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IRepositoryManager _manager;

    public ServiceManager(IRepositoryManager manager,UserManager<AppUser> userManager, IOptions<JwtSettings> jwtOptions)
    {
        _manager = manager;
        _userManager = userManager;

        _userLessonProgressManager = new Lazy<IUserLessonProgressManager>(() => new UserLessonProgressManager(manager));
        _authenticationManager = new Lazy<IAuthenticationManager>(() => new AuthenticationManager(userManager, jwtOptions));
        _userActivityHistoryManager = new Lazy<IUserActivityHistoryManager>(() => new UserActivityHistoryManager(manager));
        _userQuestionStatManager = new Lazy<IUserQuestionStatManager>(() => new UserQuestionStatManager(manager));
        _profileStatManager = new Lazy<IProfileStatManager>(() => new ProfileStatManager(manager));
    }
    public IUserLessonProgressManager UserLessonProgressManager => _userLessonProgressManager.Value;    

    public IAuthenticationManager AuthenticationManager => _authenticationManager.Value;

    public IUserActivityHistoryManager UserActivityHistoryManager => _userActivityHistoryManager.Value;

    public IUserQuestionStatManager UserQuestionStatManager => _userQuestionStatManager.Value;

    public IProfileStatManager ProfileStatManager => _profileStatManager.Value;
}
