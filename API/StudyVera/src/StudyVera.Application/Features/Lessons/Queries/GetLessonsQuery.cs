using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudyVera.Application.Dtos.Lessons;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.Lessons.Queries;

public class GetLessonsQuery : IRequest<List<LessonDto>>
{
    [BindNever]
    public TargetExam TargetExam { get; set; }
}
