using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.Lessons.Commands;

public record AddLessonCommand(string Name,int ExamId) : IRequest<int>;
