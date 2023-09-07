namespace PerfumeShop.Web.Areas.Shop.Pages;

[Area("Shop")]
[Authorize]
public class OrderSuccessModel : PageModel
{
    private readonly IOrderQueryService _orderQueryService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

	public OrderSuccessModel(
        IOrderQueryService orderQueryService,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _orderQueryService = orderQueryService;
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
            }         
        }           
    }

    public async Task<IActionResult> OnPostOrderIdAsync(string trackingId)
    {
        var orderId = await _orderQueryService.GetOrderIdAsync(trackingId);
        return Redirect($"/OrderHistory/Details?id={orderId}");
    }
}
