using MediatR;
using StudyVera.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudyVera.Application.Features.UserLessonProgresses.Commands;

public class ReviewTopicCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    [JsonIgnore]
    [Required(ErrorMessage = "ulpId is required.")]
    public int ulpId { get; set; }

    public int? DurationMinutes { get; set; }
}