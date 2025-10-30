using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.UserQuestionStats.Commands
{
    public class SolveQuestionCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public int TopicId { get; set; }
        public int SolvedCount { get; set; }
        public int CorrectCount { get; set; }
        public int WrongCount { get; set; }
    }

}
