using MediatR;
using StudyVera.Contract.Dtos;
using StudyVera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.Topics.Queries
{
    public class GetAllTopicsQuery : IRequest<List<TopicDto>>
    {

    }

}
