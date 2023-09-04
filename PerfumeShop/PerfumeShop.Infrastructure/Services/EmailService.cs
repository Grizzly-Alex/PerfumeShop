namespace PerfumeShop.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private readonly ILogger<PaymentService> _logger;

    public EmailService(
        IOptions<EmailSettings> settings,
        ILogger<PaymentService> logger)
    {
        _settings = settings.Value;
        _logger = logger;

    }

    public string GetEmailTemplate<T>(string emailTemplateName, T emailTemplateModel)
    {
        string emailTemplate = LoadTemplate(emailTemplateName);
        IRazorEngine razorEngine = new RazorEngine();
        IRazorEngineCompiledTemplate modifiedMailTemplate = razorEngine.Compile(emailTemplate);

        return modifiedMailTemplate.Run(emailTemplateModel);
    }

    private string LoadTemplate(string emailTemplate)
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string templateDir = Path.Combine(baseDir, "Views/EmailTemplates");
        string templatePath = Path.Combine(templateDir, $"{emailTemplate}.cshtml");

        using FileStream fileStream = new(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using StreamReader streamReader = new(fileStream, Encoding.Default);

        string mailTemplate = streamReader.ReadToEnd();
        streamReader.Close();

        return mailTemplate;
    }

    public async Task<bool> SendEmailAsync(EmailData emailData, CancellationToken ct = default)
    {
        try
        {
            var email = CreateEmail(emailData);
            await SendAsync(email, ct);

            _logger.LogInformation($"Mail has successfully been sent.");

            return true;
        }
        catch (EmailException)
        {
            _logger.LogError($"An error occured. The Mail could not be sent.");

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

    private BodyBuilder CreateBodyEmail(string html, IFormFileCollection? attachments)
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
                if (attachment.Length > 0)
                {
                    using (MemoryStream memoryStream = new())
                    {
                        attachment.CopyTo(memoryStream);
                        attachmentFileByteArray = memoryStream.ToArray();
                    }

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
