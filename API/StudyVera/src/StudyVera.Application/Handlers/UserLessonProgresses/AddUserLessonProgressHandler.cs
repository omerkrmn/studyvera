using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Features.UserLessonProgresses.Commands;
using StudyVera.Contract.Dtos.UserLessonProgress;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Exceptions;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.UserLessonProgresses
{
    public class AddUserLessonProgressHandler : IRequestHandler<AddUserLessonProgressCommand, UserLessonProgressDto>
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public AddUserLessonProgressHandler(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<UserLessonProgressDto> Handle(AddUserLessonProgressCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ParameterNullException(nameof(request));

            var ulp = await _manager.UserLessonProgressRepository
                           .FindByCondition(a => a.UserId == request.UserId && a.TopicId == request.TopicId, true)
                           .Include(a => a.Topic)
                           .ThenInclude(t => t.Lesson)
                           .FirstOrDefaultAsync(cancellationToken);

            if (ulp is not null)
            {
                if (request.ProgressStatus != ulp.ProgressStatus)
                {
                    ulp.ProgressStatus = request.ProgressStatus;
                    ulp.LastUpdated = DateTime.UtcNow;

                    _manager.UserLessonProgressRepository.Update(ulp);
                }
                else
                {
                    throw new BadRequestException("The progress status is the same as the current one.");
                }
            }
            else
            {
                var ulpNew = _mapper.Map<UserLessonProgress>(request);
                ulpNew.LastUpdated = DateTime.UtcNow;

                _manager.UserLessonProgressRepository.Create(ulpNew);
                ulp = ulpNew;
            }

            var activity = new UserActivityHistory
            {
                UserId = request.UserId,
                ActivityType = Domain.Enums.ActivityType.LessonProgressed,
                ActivityDate = DateTime.UtcNow,
                Description = $"User added lesson progress for TopicId: {request.TopicId} with Status: {request.ProgressStatus}"
            };
            _manager.UserActivityHistoryRepository.Create(activity);

            await _manager.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserLessonProgressDto>(ulp);
        }

    }

}
