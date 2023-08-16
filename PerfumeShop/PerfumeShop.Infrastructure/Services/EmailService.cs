namespace PerfumeShop.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public Task<bool> SendEmailAsync(EmailData emailData, CancellationToken ct = default)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(_settings.DisplayName, emailData.DisplayName ?? _settings.From));
        email.Sender = new MailboxAddress(emailData.DisplayName ?? _settings.DisplayName, emailData.From ?? _settings.From);

        emailData.To.ForEach(mailAddress => email.To.Add(MailboxAddress.Parse(mailAddress)));

        if (!string.IsNullOrEmpty(emailData.ReplyTo))
            email.ReplyTo.Add(new MailboxAddress(emailData.ReplyToName, emailData.ReplyTo));

        return default;
    }
}
