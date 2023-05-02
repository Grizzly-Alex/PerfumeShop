namespace Microsoft.eShopWeb.Web.Areas.Identity.Pages;

[AllowAnonymous]
public class ConfirmEmailModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;

    public ConfirmEmailModel(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
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

        return Page();
    }
}
