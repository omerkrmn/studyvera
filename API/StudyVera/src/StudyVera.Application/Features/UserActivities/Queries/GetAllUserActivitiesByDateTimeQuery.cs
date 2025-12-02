using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.UserActivities.Queries;

public class GetAllUserActivitiesByDateTimeQuery : IRequest<List<DateTime>>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
}
