using MediatR;
using StudyVera.Contract.Dtos.UserLessonProgress;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.UserLessonProgresses.Commands
{
    public class AddUserLessonProgressCommand : IRequest<UserLessonProgressDto>
    {
        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "TopicId is required.")]
        public int TopicId { get; set; }

        [Required(ErrorMessage = "ProgressStatus is required.")]
        public ProgressStatus ProgressStatus { get; set; }
    }

}
