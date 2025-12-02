using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.UserQuestionStats.Commands
{
    public class AddUserQuestionStatCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public int TopicId { get; set; }

        public int SolvedCount { get; set; }
        public int CorrectCount { get; set; }

    }   

}
