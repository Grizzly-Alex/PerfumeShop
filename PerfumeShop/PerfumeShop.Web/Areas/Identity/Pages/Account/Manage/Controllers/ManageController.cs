using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;

namespace PerfumeShop.Web.Areas.Identity.Pages.Account.Manage.Controllers;

public class ManageController : Controller
{
    [TempData]
    public string? StatusMessage { get; set; }

    private readonly ILogger<ManageController> _logger;
    private readonly IEmailSender _emailSender;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UrlEncoder _urlEncoder;

    private const string RecoveryCodesKey = nameof(RecoveryCodesKey);
    private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

    public ManageController(
        ILogger<ManageController> logger,
        IEmailSender emailSender,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        UrlEncoder urlEncoder)
    {
        _logger = logger;
        _emailSender = emailSender;
        _userManager = userManager;
        _signInManager = signInManager;
        _urlEncoder = urlEncoder;
    }
    public async Task<IActionResult> MyAccount()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var model = new IndexViewModel
        {
            Username = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            IsEmailConfirmed = user.EmailConfirmed,
            StatusMessage = StatusMessage
        };

        return View();
    }
}
