using MediatR;
using StudyVera.Application.Dtos.UserActivityHistory;
using System.Text.Json.Serialization;

namespace StudyVera.Application.Features.UserActivities.Queries
{
    public class GetAllUserActivityHistoryByUserQuery : IRequest<List<UserActivityHistoryDto>>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

    }

}
