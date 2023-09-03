using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace PerfumeShop.Web.Areas.Identity.Pages;

[AllowAnonymous]
public class RegisterConfirmationModel : PageModel
{
    private readonly ILogger<RegisterConfirmationModel> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;

    public RegisterConfirmationModel(
        ILogger<RegisterConfirmationModel> logger,
        IEmailService emailService,
        UserManager<AppUser> userManager)
    {
        _logger = logger;   
        _emailService = emailService;
        _userManager = userManager;
    }

    public ResultViewModel Result { get; set; }

    public async Task<IActionResult> OnGetAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return NotFound($"Unable to load user with email '{email}'.");
        }

        bool result = await SendEmailAsync(user);

        Result = new()
        {
            Success = result,
            Notifocation = new()
            {
                Status = result
                    ? NotificationStatus.Info
                    : NotificationStatus.Error,
                Text = result
                    ? $"Please check your email {email} to confirm your account."
                    : $"Error to send confirmation email {email}."
            }
        };

        return Page();
    }

    private async Task<bool> SendEmailAsync(AppUser user)
    {
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var callbackUrl = Url.Page(
            "./ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId = user.Id, code = code },
            protocol: Request.Scheme);

        return await _emailService.SendEmailConfirmationAsync(
            new ConfirmationEmailViewModel
            {
                Email = user.Email!,
                ConfirmationLink = HtmlEncoder.Default.Encode(callbackUrl)
            });
    }
}
