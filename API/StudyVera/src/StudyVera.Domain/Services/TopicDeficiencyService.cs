using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Services;

public class TopicDeficiencyService : ITopicDeficiencyService
{
    public float CalculateDeficiencyScore(int totalSolvedCount, int correctCount, DateTime lastAttemptAt, DateTime lastWorkingDate, float topicPriority)
    {
        const float expectedSuccessRate = 0.60f;
        const float successWeight = 0.6f;
        const float recencyWeight = 0.2f;
        const float workingRecencyWeight = 0.2f;
        const int confidenceThreshold = 10;
        const int recencyLimitDays = 90;

        int daysSinceLastAttempt = (DateTime.Now - lastAttemptAt).Days;
        int daysSinceLastWorkingDate = (DateTime.Now - lastWorkingDate).Days;

        float recencyScore = Math.Min(daysSinceLastAttempt, recencyLimitDays);
        float workingRecencyScore = Math.Min(daysSinceLastWorkingDate, recencyLimitDays);

        float adjustedSuccessRatio = ((float)correctCount + (confidenceThreshold * expectedSuccessRate))
                                    / (totalSolvedCount + confidenceThreshold);

        float successPercentage = adjustedSuccessRatio * 100f;
        float successLossScore = 100f - successPercentage;

        float deficiencyScore = (successWeight * successLossScore) + (recencyWeight * recencyScore) + (workingRecencyWeight * workingRecencyScore);

        return deficiencyScore * topicPriority;
    }

    public float CalculateDeficiencyScoreVersion1(int totalSolvedCount, int correctCount, DateTime lastAttemptAt, float topicPriority)
    {
        const float expectedSuccessRate = 0.60f;
        const float successWeight = 0.7f;
        const float recencyWeight = 0.3f;
        const int confidenceThreshold = 10;
        const int recencyLimitDays = 90;

        int daysSinceLastAttempt = (DateTime.Now - lastAttemptAt).Days;

        float recencyScore = Math.Min(daysSinceLastAttempt, recencyLimitDays);

        float adjustedSuccessRatio = ((float)correctCount + (confidenceThreshold * expectedSuccessRate))
                                    / (totalSolvedCount + confidenceThreshold);

        float successPercentage = adjustedSuccessRatio * 100f;
        float successLossScore = 100f - successPercentage;

        float deficiencyScore = (successWeight * successLossScore) + (recencyWeight * recencyScore);

        return deficiencyScore * topicPriority;
    }
}
