using MediatR;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.UserLessonProgresses.Commands;

public class UpdateUserLessonProgressCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    [JsonIgnore]
    [Required(ErrorMessage = "TopicId is required.")]
    public int TopicId { get; set; }
    [Required(ErrorMessage = "TopicId is required.")]
    public ProgressStatus ProgressStatus { get; set; }
}
