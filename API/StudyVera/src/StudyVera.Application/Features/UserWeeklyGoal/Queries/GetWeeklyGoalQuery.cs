using MediatR;
using StudyVera.Application.Dtos.UserWeeklyGoals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.UserWeeklyGoal.Queries;

public class GetWeeklyGoalQuery : IRequest<UserWeeklyGoalDto>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
}
