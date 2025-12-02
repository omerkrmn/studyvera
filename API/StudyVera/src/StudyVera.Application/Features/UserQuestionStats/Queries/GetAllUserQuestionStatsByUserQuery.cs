using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudyVera.Application.Common.Models;
using StudyVera.Application.Dtos.UserQuestionStats;
using System.Text.Json.Serialization;

namespace StudyVera.Application.Features.UserQuestionStats.Queries
{
    public class GetAllUserQuestionStatsByUserQuery :RequestParameters, IRequest<PagedList<UserQuestionStatDto>>
    {
        [JsonIgnore]
        [BindNever]
        public Guid UserId { get; set; }

    }

}
