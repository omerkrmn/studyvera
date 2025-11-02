using AutoMapper;
using Mapster;
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

        public AddUserLessonProgressHandler(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<UserLessonProgressDto> Handle(AddUserLessonProgressCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ParameterNullException(nameof(request));
            UserLessonProgressDto response;

            var existingProgress = await _manager.UserLessonProgressRepository
                .FindByCondition(
                    a => a.UserId == request.UserId && a.TopicId == request.TopicId,
                    trackChanges: false).AnyAsync();




            var activity = new UserActivityHistory
            {
                UserId = request.UserId,
                ActivityType = Domain.Enums.ActivityType.LessonProgressed,
                ActivityDate = DateTime.UtcNow,
                Description = $"User updated lesson progress for Topic ID {request.TopicId} → Status: {request.ProgressStatus}"
            };

            _manager.UserActivityHistoryRepository.Create(activity);

            await _manager.SaveChangesAsync(cancellationToken);
            return existingProgress.Adapt<UserLessonProgressDto>();
        }


    }

}
