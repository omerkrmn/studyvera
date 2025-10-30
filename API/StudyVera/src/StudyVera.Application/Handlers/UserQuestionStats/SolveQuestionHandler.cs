using MediatR;
using StudyVera.Application.Features.UserQuestionStats.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.UserQuestionStats
{
    public class SolveQuestionHandler : IRequestHandler<SolveQuestionCommand, bool>
    {
        private readonly IRepositoryManager _manager;

        public SolveQuestionHandler(IRepositoryManager manager)
        {
            _manager = manager;
        }
        public async Task<bool> Handle(SolveQuestionCommand request, CancellationToken ct)
        {
            var existing = _manager.UserQuestionStatRepository
               .FindByCondition(
                                   x => x.UserId == request.UserId && x.TopicId == request.TopicId,
                                   trackChanges: true);
            var stat = existing.FirstOrDefault();
            if (stat == null)
            {
                stat = new UserQuestionStat
                {
                    UserId = request.UserId,
                    TopicId = request.TopicId,
                    SolvedCount = request.SolvedCount,
                    CorrectCount = request.CorrectCount,
                    WrongCount = request.WrongCount,
                    LastAttemptAt = DateTime.UtcNow
                };
               

                _manager.UserQuestionStatRepository.Create(stat);
            }
            else
            {
                stat.SolvedCount += request.SolvedCount;
                stat.CorrectCount += request.CorrectCount;
                stat.WrongCount += request.WrongCount;
                stat.LastAttemptAt = DateTime.UtcNow;
            }

            await _manager.UserActivityHistoryRepository
                             .AddAsync(
                                   request.UserId,
                                   ActivityType.SolvedAQuestion,
                                   $"topicId:{request.TopicId} - solvedCount:{request.SolvedCount}",
                                   ct);
            await _manager.SaveChangesAsync(ct);
            return true;
        }
    }

}
