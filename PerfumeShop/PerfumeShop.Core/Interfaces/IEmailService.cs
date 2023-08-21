namespace PerfumeShop.Core.Interfaces;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailData emailData, CancellationToken ct = default);
    string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel);
}
