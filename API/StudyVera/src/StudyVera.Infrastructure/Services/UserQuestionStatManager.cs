using StudyVera.Contract.Services;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Services;

public class UserQuestionStatManager : IUserQuestionStatManager
{
    private readonly IRepositoryManager _manager;

    public UserQuestionStatManager(IRepositoryManager manager)
    {
        _manager = manager;
    }
}
