using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.Lessons.Commands
{
    public record AddRangeLessonCommand(Dictionary<string,int> List) : IRequest<List<int>>;

}
