using MediatR;
using StudyVera.Application.Dtos.QuestionStatDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.QuestionStatDetails.Queries;

public class GetAllByUserQuestionStatQuery : IRequest<List<QuestionStatDetailDto>>
{
    public int QuestionStatId { get; set; }
}
