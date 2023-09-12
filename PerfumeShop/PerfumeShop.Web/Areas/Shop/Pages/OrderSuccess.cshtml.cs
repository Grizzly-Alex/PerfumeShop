using Microsoft.IdentityModel.Tokens;

namespace PerfumeShop.Web.Areas.Shop.Pages;

[Area("Shop")]
[Authorize]
public class OrderSuccessModel : PageModel
{
    private readonly IOrderQueryService _orderQueryService;
    private readonly IOrderViewModelService _orderViewModelService;
    private readonly IEmailService _emailService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

	public OrderSuccessModel(
        IOrderQueryService orderQueryService,
        IOrderViewModelService orderViewModelService,
        IEmailService emailService,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _orderQueryService = orderQueryService;
        _orderViewModelService = orderViewModelService;
        _emailService = emailService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public string EmailModel { get; set; } = new string(string.Empty);
    public string OrderTrackingId { get; set; } = new string(string.Empty);


    public async Task OnGetAsync()
    {
        if (_signInManager.IsSignedIn(HttpContext.User))
        {           
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            EmailModel = user.Email;

            if (HttpContext.Session.Keys.Contains(Constants.SESSION_ORDER_TRACKING_ID))
            {
                OrderTrackingId = HttpContext.Session.Get<String>(Constants.SESSION_ORDER_TRACKING_ID)!;
                await SendOrderToEmailAsync(OrderTrackingId);
            }         
        }           
    }

    public async Task<IActionResult> OnPostOrderIdAsync(string trackingId)
    {
        if (trackingId.IsNullOrEmpty())
        {
            return Page();
        }
        else
        {
            var orderId = await _orderQueryService.GetOrderIdAsync(trackingId);
            return Redirect($"/OrderHistory/Details?id={orderId}");
        }
    }

    private async Task<bool> SendOrderToEmailAsync(string trackingId)
    {
        var orderId = await _orderQueryService.GetOrderIdAsync(trackingId);
        var orderEmail = await _orderViewModelService.GetOrderEmailViewModelAsync(orderId);

        return await _emailService.SendEmailOrderAsync(orderEmail);
    }
}
