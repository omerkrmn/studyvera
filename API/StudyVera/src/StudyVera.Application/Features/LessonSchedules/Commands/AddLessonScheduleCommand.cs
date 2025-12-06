using MediatR;
using StudyVera.Application.Dtos.LessonSchedules;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.LessonSchedules.Commands;

public class AddLessonScheduleCommand : IRequest<Unit>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public int LessonId { get; set; }
    public int TopicId { get; set; }
    public int DayOfWeek { get; set; }
    public TimeSpan? StartTime { get; set; } 
    public TimeSpan? EndTime { get; set; }
}
