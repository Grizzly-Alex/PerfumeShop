namespace PerfumeShop.Infrastructure.Services;

public class EmailSender : IEmailSender
{
	public async Task SendEmailAsync(string email, string subject, string htmlMessage)
	{
        using var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Perfume Shop", "grizzlydemo@mail.ru"));
        emailMessage.To.Add(MailboxAddress.Parse(email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.mail.ru", 2525, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("grizzlydemo@mail.ru", "yarmhxxk7fcznwbVHuyW");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
