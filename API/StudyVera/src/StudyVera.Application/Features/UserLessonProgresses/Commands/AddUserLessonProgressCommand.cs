using MediatR;
using StudyVera.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudyVera.Application.Features.UserLessonProgresses.Commands;

public class AddUserLessonProgressCommand : IRequest<Unit>
{

    [JsonIgnore]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "TopicId is required.")]
    public int TopicId { get; set; }

    [Required(ErrorMessage = "ProgressStatus is required.")]
    public ProgressStatus ProgressStatus { get; set; }
}
