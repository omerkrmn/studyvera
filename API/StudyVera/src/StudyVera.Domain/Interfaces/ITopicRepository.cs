using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Interfaces;

public interface ITopicRepository
{
    public Task<List<Topic>> GetAllByTargetAsync(ExamTarget exam, CancellationToken ct = default);
    public Task<List<Topic>> GetAllByLessonIdAsync(ExamTarget exam,int lessonId, CancellationToken ct = default);
    public void AddRange(List<Topic> topics);
    public void Add(Topic topic);
    public void Remove(Topic topic);
    public void Delete(Topic topic);
}
