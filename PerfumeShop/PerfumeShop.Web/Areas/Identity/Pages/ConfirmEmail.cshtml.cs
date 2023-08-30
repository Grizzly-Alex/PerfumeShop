namespace Microsoft.eShopWeb.Web.Areas.Identity.Pages;

[AllowAnonymous]
public class ConfirmEmailModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public ConfirmEmailModel(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> OnGetAsync(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return RedirectToPage("/Index");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        return Page();
    }
}
