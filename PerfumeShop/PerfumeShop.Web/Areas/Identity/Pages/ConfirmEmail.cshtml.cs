using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace Microsoft.eShopWeb.Web.Areas.Identity.Pages;

[AllowAnonymous]
public class ConfirmEmailModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;

    public ConfirmEmailModel(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
    }

    //[TempData]
    //public string StatusMessage { get; set; }

    [BindProperty]
    public ResultViewModel Result { get; set; }


    public async Task<IActionResult> OnGetAsync(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return RedirectToPage("/Index");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);

        Result = new()
        {
            Success = result.Succeeded,
            StatusMessage = result.Succeeded 
                ? $"Thank you for confirming your email: {user.Email}. Now you can login to your account."
                : $"Error confirming your email: {user.Email}"
        };

        return Page();
    }
}
