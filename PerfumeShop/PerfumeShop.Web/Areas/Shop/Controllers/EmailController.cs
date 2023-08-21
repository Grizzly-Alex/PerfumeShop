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
    public async Task SendWelcomeEmail(WelcomeEmail welcomeEmail)
    {
        var mailData = new EmailData(
            "Welcome to the Perfume Shop",
            new List<string> { welcomeEmail.Email },
            _emailService.GetEmailTemplate("welcome", welcomeEmail));

        bool result = await _emailService.SendEmailAsync(mailData, new CancellationToken());
    }
}
