namespace PerfumeShop.Web.Extensions;

public static class EmailServiceExtensions
{
    public static async Task<bool> SendEmailConfirmationAsync(this IEmailService emailService, ConfirmationEmailViewModel confirmationEmail)
    {
        var mailData = new EmailData(
            "Perfume Shop",
            new List<string> { confirmationEmail.Email },
            emailService.GetEmailTemplate("Confirmation", confirmationEmail));

        return await emailService.SendEmailAsync(mailData, new CancellationToken());
    }
}