using StudyVera.FrontEnd.ComponentModel.DataAnnotations;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface IEmailService
{
    Task<bool> SendContactEmail(ContactFormModel model);
}
