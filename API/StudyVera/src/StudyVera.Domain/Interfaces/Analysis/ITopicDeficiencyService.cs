using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudyVera.Domain.Interfaces.Analysis;

public interface ITopicDeficiencyService
{
    /// <summary>
    /// Calculates the deficiency score for a given topic based on the total solved count, correct count, last attempt date, and topic priority.
    /// </summary>
    /// <param name="totalSolvedCount">Total Solved Count by topic</param>
    /// <param name="correctCount">Total Correct Count by topic</param>
    /// <param name="lastAttemptAt">date the problem was solved</param>
    /// <param name="topicPriority"></param>
    /// <returns></returns>
    public float CalculateDeficiencyScore(int totalSolvedCount, int correctCount, DateTime lastAttemptAt, float topicPriority);
}
