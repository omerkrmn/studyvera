using StudyVera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Interfaces;

public interface IQuestionStatDetailRepository : IRepository<QuestionStatDetail>
{
    /// <summary>
    /// Get sum of TotalSolvedCount and TotalCorrectCount by QuestionStatDetailId
    /// </summary>
    /// <param name="questionStatDetailId"></param>
    /// <returns></returns>
    public (Task<int> TotalSolvedCount,  Task<int> TotalCorrectCount) GetSum(int questionStatDetailId);

}
