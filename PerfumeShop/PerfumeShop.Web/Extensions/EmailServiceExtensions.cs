namespace PerfumeShop.Web.Extensions;

public static class EmailServiceExtensions
{
    public static async Task<bool> SendEmailConfirmationAsync(this IEmailService emailService, ConfirmationEmailViewModel confirmationEmail)
    {
        var mailData = new EmailData(
            subject: "Perfume Shop",
            to: new List<string> { confirmationEmail.Email },
            body: emailService.GetEmailTemplate("Confirmation", confirmationEmail));

        return await emailService.SendEmailAsync(mailData, new CancellationToken());
    }

    public static async Task<bool> SendEmailOrderAsync(this IEmailService emailService, OrderViewModel order)
    {
        var mailData = new EmailData(
            subject: "Perfume Shop",
            to: new List<string> { order.OrderInfo.CustomerEmail },
            body: emailService.GetEmailTemplate("Order", order));

        return await emailService.SendEmailAsync(mailData, new CancellationToken());
    }
}