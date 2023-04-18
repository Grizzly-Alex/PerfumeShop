namespace Microsoft.eShopWeb.Web.Areas.Identity.Pages.Account;

public class LogoutModel : PageModel
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ILogger<LogoutModel> _logger;

    public LogoutModel(
        SignInManager<AppUser> signInManager,
        ILogger<LogoutModel> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(string? returnUrl = null)
    {
        await _signInManager.SignOutAsync();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        _logger.LogInformation("User logged out.");
        if (returnUrl is not null)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            return RedirectToPage("/Index");
        }
    }
}
