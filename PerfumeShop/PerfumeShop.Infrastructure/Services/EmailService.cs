namespace PerfumeShop.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<bool> SendEmailAsync(EmailData emailData, CancellationToken ct = default)
    {
        try
        {
            var message = new MimeMessage();


            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }   

    private void GetSender(MimeMessage message, EmailData emailData)
    {
        message.From.Add(new MailboxAddress(_settings.DisplayName, emailData.DisplayName ?? _settings.From));
        message.Sender = new MailboxAddress(emailData.DisplayName ?? _settings.DisplayName, emailData.From ?? _settings.From);
    }

    private void CreateReseiver(MimeMessage message, EmailData emailData)
    {
        emailData.To.ForEach(mailAddress => message.To.Add(MailboxAddress.Parse(mailAddress)));
    }

    private void SetBcc(MimeMessage message, EmailData emailData) =>
        emailData.Bcc?
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList()
                .ForEach(mailAddress => message.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim())));

    private void SetCc(MimeMessage message, EmailData emailData) =>
        emailData.Cc?
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList()
                .ForEach(mailAddress => message.Cc.Add(MailboxAddress.Parse(mailAddress.Trim())));

    private void AddContentToMessage(MimeMessage message, EmailData emailData)
    {
        var body = new BodyBuilder();
        message.Subject = emailData.Subject;
        body.HtmlBody = emailData.Body;
        message.Body = body.ToMessageBody();
    }
    
    private async Task SendEmail(MimeMessage message, CancellationToken ct = default)
    {
        using var smtp = new SmtpClient();
        if (_settings.UseSSL)
        {
            await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect, ct);
        }
        else if (_settings.UseStartTls)
        {
            await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
        }
        await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
        await smtp.SendAsync(message, ct);
        await smtp.DisconnectAsync(true, ct);
    }
}
