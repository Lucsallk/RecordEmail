using RecordEmail.Models;

namespace RecordEmail.Services
{
    public interface IEmailServices
    {
        Task<bool> SendEmailAsync(EmailData emailData, CancellationToken ct);
        string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel);
    }
}
