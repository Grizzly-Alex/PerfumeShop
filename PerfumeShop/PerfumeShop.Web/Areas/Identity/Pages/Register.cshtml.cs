namespace Microsoft.eShopWeb.Web.Areas.Identity.Pages;

[AllowAnonymous]
public class RegisterModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ILogger<RegisterModel> _logger;

    public RegisterModel(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ILogger<RegisterModel> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    [BindProperty]
    public RegisterUserViewModel? Input { get; set; }
    public string? ReturnUrl { get; set; }


    public void OnGet(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        if (ModelState.IsValid)
        {
            var user = new AppUser
            {
                UserName = Input.Email,
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                StreetAddress = Input.StreetAddress,
                City = Input.City,
                State = Input.State,
                PostalCode = Input.PostalCode,
                PhoneNumber = Input.PhoneNumber,
                Email = Input.Email,               
            };
            var result = await _userManager.CreateAsync(user, Input.Password);            
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());               

                if (_userManager.Options.SignIn.RequireConfirmedEmail)
                {
                    return RedirectToPage("./RegisterConfirmation", new { email = user.Email });
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return Page();
    }
}
