using StudyVera.FrontEnd.ComponentModel.DataAnnotations;
using StudyVera.FrontEnd.Services.Concrats;
using System.Net;
using System.Net.Mail;

namespace StudyVera.FrontEnd.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger; 

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger; 
    }

    public async Task<bool> SendContactEmail(ContactFormModel model)
    {
        var host = _configuration["EmailSettings:SmtpHost"];
        var portStr = _configuration["EmailSettings:SmtpPort"];
        var user = _configuration["EmailSettings:SmtpUser"];
        var password = _configuration["EmailSettings:SmtpPassword"];
        var toEmail = _configuration["EmailSettings:AdminEmail"];
        var fromEmail = _configuration["EmailSettings:FromEmail"];

        if (!int.TryParse(portStr, out int port))
        {
            _logger.LogError("EmailSettings:SmtpPort değeri geçersiz.");
            return false;
        }

        var subject = $"İletişim Formu: {model.Subject}";
        var body = $@"
            <h3>Yeni İletişim Mesajı</h3>
            <p><b>Gönderen Adı:</b> {model.Name}</p>
            <p><b>Gönderen E-postası:</b> <a href='mailto:{model.Email}'>{model.Email}</a></p>
            <p><b>Konu:</b> {model.Subject}</p>
            <hr>
            <p><b>Mesaj:</b></p>
            <div style='border: 1px solid #eee; padding: 15px; background-color: #f9f9f9;'>{model.Message}</div>";

        try
        {
            using (var client = new SmtpClient(host))
            {
                client.Port = port;
                client.EnableSsl = true; 
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false; 
                client.Credentials = new NetworkCredential(user, password);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail, "Web Sitesi İletişim Formu"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
            }

            _logger.LogInformation($"E-posta başarıyla gönderildi. Konu: {model.Subject}");
            return true;
        }
        catch (SmtpException smtpEx)
        {
            _logger.LogError(smtpEx, $"SMTP Hatası ({smtpEx.StatusCode}): E-posta gönderilemedi. Gönderen: {model.Email}");
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Beklenmeyen bir hata oluştu. E-posta gönderilemedi. Gönderen: {model.Email}");
            return false;
        }
    }
}