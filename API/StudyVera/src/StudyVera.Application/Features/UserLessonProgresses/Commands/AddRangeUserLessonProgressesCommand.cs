using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.UserLessonProgresses.Commands;

public class AddRangeUserLessonProgressesCommand : IRequest<bool>
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    [Required]
    public List<AddUserLessonProgressCommand> LessonProgresses { get; set; } = new();
}
