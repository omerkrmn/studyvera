using StudyVera.FrontEnd.Models.QuestionStatDetails;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface IQuestionStatDetailService
{
    public Task<List<QuestionStatDetailDto>> GetAll(int questionStatId);
}
