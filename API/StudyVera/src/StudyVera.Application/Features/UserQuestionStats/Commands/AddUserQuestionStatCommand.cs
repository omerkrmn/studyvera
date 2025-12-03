using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "TopicId is required")]
        public int TopicId { get; set; }
        [Required(ErrorMessage = "SolvedCount is required")]
        public int SolvedCount { get; set; }
        [Required(ErrorMessage = "Correct is required")]
        public int CorrectCount { get; set; }

    }   

}
