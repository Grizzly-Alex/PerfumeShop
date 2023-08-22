namespace PerfumeShop.Web.Areas.Shop.Controllers;


public class EmailController : Controller
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;            
    }


    [HttpPost]
    public async Task<IActionResult> SendEmailAsync(EmailData emailData)
    {

        bool result = await _emailService.SendEmailAsync(emailData, new CancellationToken());

        //TODO

        return View();
    }

    [HttpPost]
    public async Task SendWelcomeEmail(ConfirmationEmailViewModel confirmationEmail)
    {
        var mailData = new EmailData(
            "Welcome to the Perfume Shop",
            new List<string> { confirmationEmail.Email },
            _emailService.GetEmailTemplate("confirmation", confirmationEmail));

        bool result = await _emailService.SendEmailAsync(mailData, new CancellationToken());
    }
}
