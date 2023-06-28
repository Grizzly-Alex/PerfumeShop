using PerfumeShop.Web.ViewModels.Order;

namespace PerfumeShop.Web.Areas.Shop.Pages;

[Area("Shop")]
[Authorize]
public class OrderSuccessModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IOrderViewModelService _orderViewModelService;

    public OrderSuccessModel(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IOrderViewModelService orderViewModelService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _orderViewModelService = orderViewModelService;
    }

    public string EmailModel { get; set; } = new string(string.Empty);
    public IList<OrderItemViewModel> OrderItemViewModels { get; set; }
    public OrderInfoViewModel OrderInfoViewModel { get; set; }

    public async Task OnGet(int orderId)
    {
        if (_signInManager.IsSignedIn(HttpContext.User))
        {
            OrderItemViewModels = await _orderViewModelService.GetOrderItemModelCollectionAsync(orderId);
            OrderInfoViewModel = await _orderViewModelService.GetOrderInfoModelAsync(orderId);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            EmailModel = user.Email;
        }           
    }
}
