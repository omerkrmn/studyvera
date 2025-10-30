using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Features.Topics.Queries;
using StudyVera.Contract.Dtos;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Topics
{
    public class GetAllTopicsHandler : IRequestHandler<GetAllTopicsQuery, List<TopicDto>>
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;
        public GetAllTopicsHandler(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<List<TopicDto>> Handle(GetAllTopicsQuery request, CancellationToken cancellationToken)
        {
            var allTopics = await _manager.TopicRepository.FindAll(false).ToListAsync(cancellationToken);
            return _mapper.Map<List<TopicDto>>(allTopics);
        }
    }

}
