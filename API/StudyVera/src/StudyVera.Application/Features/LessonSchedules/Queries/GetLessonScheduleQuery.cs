using MediatR;
using StudyVera.Application.Dtos.LessonSchedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.LessonSchedules.Queries;

public class GetLessonScheduleQuery : IRequest<List<LessonScheduleDto>>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
}
