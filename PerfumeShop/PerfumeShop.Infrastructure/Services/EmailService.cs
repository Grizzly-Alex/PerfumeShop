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
            await SendAsync(CreateEmail(emailData), ct);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }   
  
    private MimeMessage CreateEmail(EmailData emailData)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(_settings.DisplayName, emailData.From ?? _settings.From));
        email.Sender = new MailboxAddress(emailData.DisplayName ?? _settings.DisplayName, emailData.From ?? _settings.From);

        emailData.To.ForEach(emailAddress => email.To.Add(MailboxAddress.Parse(emailAddress)));

        emailData.Bcc?
           .Where(x => !string.IsNullOrWhiteSpace(x))
           .ToList()
           .ForEach(mailAddress => email.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim())));

        emailData.Cc?
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList()
            .ForEach(mailAddress => email.Cc.Add(MailboxAddress.Parse(mailAddress.Trim())));

        email.Subject = emailData.Subject;

        email.Body = CreateBodyEmail(emailData.Body ?? string.Empty, emailData.Attachments).ToMessageBody();

        return email;
    } 

    private BodyBuilder CreateBodyEmail(string html, IFormFileCollection attachments)
    {
        var body = new BodyBuilder()
        {
            HtmlBody = html
        };

        if (attachments != null)
        {
            byte[] attachmentFileByteArray;

            foreach (IFormFile attachment in attachments)
            {
                // Check if length of the file in bytes is larger than 0
                if (attachment.Length > 0)
                {
                    // Create a new memory stream and attach attachment to mail body
                    using (MemoryStream memoryStream = new())
                    {
                        // Copy the attachment to the stream
                        attachment.CopyTo(memoryStream);
                        attachmentFileByteArray = memoryStream.ToArray();
                    }
                    // Add the attachment from the byte array
                    body.Attachments.Add(attachment.FileName, attachmentFileByteArray, ContentType.Parse(attachment.ContentType));
                }
            }
        }
        return body;        
    }

    private async Task SendAsync(MimeMessage email, CancellationToken ct = default)
    {
        using var smtp = new SmtpClient();
        smtp.AuthenticationMechanisms.Remove("XOAUTH2");
        if (_settings.UseSSL)
        {
            await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect, ct);
        }
        else if (_settings.UseStartTls)
        {
            await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
        }
        await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
        await smtp.SendAsync(email, ct);
        await smtp.DisconnectAsync(true, ct);
    }
}
