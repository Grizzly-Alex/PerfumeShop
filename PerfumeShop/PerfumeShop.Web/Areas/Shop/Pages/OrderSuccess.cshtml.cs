using Microsoft.IdentityModel.Tokens;
namespace PerfumeShop.Web.Areas.Shop.Pages;

[Area("Shop")]
[Authorize]
public class OrderSuccessModel : PageModel
{
    private readonly IOrderQueryService _orderQueryService;
    private readonly IBasketQueryService _basketQueryService;
    private readonly IOrderViewModelService _orderViewModelService;
    private readonly IEmailService _emailService;
    private readonly ICatalogProductService _catalogProductService;
    private readonly IBasketService _basketService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

	public OrderSuccessModel(
        IOrderQueryService orderQueryService,
        IOrderViewModelService orderViewModelService,
        IEmailService emailService,
        IBasketService basketService,
        ICatalogProductService catalogProductService,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IBasketQueryService basketQueryService)
    {
        _orderQueryService = orderQueryService;
        _orderViewModelService = orderViewModelService;
        _basketService = basketService;
        _catalogProductService = catalogProductService;
        _emailService = emailService;
        _userManager = userManager;
        _signInManager = signInManager;
        _basketQueryService = basketQueryService;

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

                var orderId = await _orderQueryService.GetOrderIdAsync(OrderTrackingId);
                var basketId = await _basketQueryService.GetBasketIdAsync(User.Identity.Name);
                var orderitems = await _orderQueryService.GetOrderItemsAsync(orderId);

                await _catalogProductService.UpdateStockAfterOrderAsync(orderitems);
                await _basketService.ClearBasketAsync(basketId);
                HttpContext.Session.Remove(Constants.BASKET_ITEMS_QTY);
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
