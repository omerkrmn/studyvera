using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudyVera.Application.Features.UserLessonProgresses.Queries;
using StudyVera.Contract.Dtos.UserLessonProgress;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.UserLessonProgresses
{
    public class GetAllLessonProgressesHandler : IRequestHandler<GetAllLessonProgressesQuery, List<UserLessonProgressDto>>
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public GetAllLessonProgressesHandler(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<List<UserLessonProgressDto>> Handle(GetAllLessonProgressesQuery request, CancellationToken cancellationToken)
        {
            var userLessonProgresses = 
                await _manager.UserLessonProgressRepository.FindByCondition(ulp => ulp.UserId == request.UserId, false)
                .Include(a=>a.Topic)
                .ToListAsync(cancellationToken);
            var response = _mapper.Map<List<UserLessonProgressDto>>(userLessonProgresses);
            return response;
        }
    }

}
