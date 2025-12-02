using MediatR;
using StudyVera.Application.Dtos;
using StudyVera.Domain.Enums;
using System.Text.Json.Serialization;

namespace StudyVera.Application.Features.Topics.Queries
{
    public class GetAllTopicsQuery : IRequest<List<TopicDto>>
    {
        [JsonIgnore]
        public TargetExam TargetExam { get; set; }
        public string? SearchTerm { get; set; }
    }

}
