using PerfumeShop.Web.ViewModels.Order;

namespace PerfumeShop.Web.Areas.Shop.Pages;

[Area("Shop")]
[Authorize]
public class OrderSuccessModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IOrderQueryService _orderQueryService;

	public OrderSuccessModel(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IOrderQueryService orderQueryService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _orderQueryService = orderQueryService;
    }

    public string EmailModel { get; set; } = new string(string.Empty);
    public string OrderNumber { get; set; } = new string(string.Empty);

    public async Task OnGet(int orderId)
    {
        if (_signInManager.IsSignedIn(HttpContext.User))
        {
            OrderNumber = await _orderQueryService.GetTrackingId(orderId);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            EmailModel = user.Email;
        }           
    }
}
