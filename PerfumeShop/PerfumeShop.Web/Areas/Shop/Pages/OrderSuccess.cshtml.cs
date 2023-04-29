namespace PerfumeShop.Web.Areas.Shop.Pages;


[Area("Shop")]
[Authorize]
public class OrderSuccessModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public OrderSuccessModel(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;

    }

    public string EmailModel { get; set; } = new string(string.Empty);

    public async Task OnGet()
    {
        if (_signInManager.IsSignedIn(HttpContext.User))
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            EmailModel = user.Email;
        }           
    }
}
