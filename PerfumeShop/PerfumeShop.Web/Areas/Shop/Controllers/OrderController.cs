using PerfumeShop.Web.ViewModels.Order;
namespace PerfumeShop.Web.Areas.Shop.Controllers;

[Area("Shop")]
[Authorize]
public class OrderController : Controller
{
    private readonly IMapper _mapper;
	private readonly IBasketViewModelService _basketViewModelService;
	private readonly IOrderViewModelService _orderViewModelService;
    private readonly IOrderService _orderService;
	private readonly IPhysicalShopQueryService _physicalShopQueryService;
    private readonly SignInManager<AppUser> _signInManager;
	private readonly UserManager<AppUser> _userManager;


	public OrderController(
		IMapper mapper,
		IBasketViewModelService basketViewModelService,
		IOrderService orderService,
		IOrderViewModelService orderViewModelService,
        IPhysicalShopQueryService physicalShopQueryService,
        SignInManager<AppUser> signInManager,
		UserManager<AppUser> userManager)
    {
		_mapper = mapper;
		_basketViewModelService = basketViewModelService;
		_orderService = orderService;
		_orderViewModelService = orderViewModelService;
		_physicalShopQueryService = physicalShopQueryService;
		_signInManager = signInManager;
		_userManager = userManager;
	}

    [HttpGet]
    public async Task<IActionResult> Index()
    {
		OrderCreateViewModel order = _signInManager.IsSignedIn(HttpContext.User)
			? await _orderViewModelService.GetOrderCreateModelForAuthorizedUserAsync(User.Identity.Name)
			: await _orderViewModelService.GetOrderCreateModelForAnonymousUserAsync(GetAnonymousUserId());

		return View(order);			
    }

    [HttpPost]
    public async Task<IActionResult> OrderingPickup(OrderCreateViewModel model)
    {
		ModelState.Remove(nameof(model.Address));

        if (ModelState.IsValid)
        {
			var deliveryAddress = await _physicalShopQueryService.GetAddress(model.PickupPointId);
            var customer = _mapper.Map<Customer>(model.Buyer);

            var order = await _orderService.CreateOrderAsync(
                (PaymentMethods)model.PaymentMethodId,
                DeliveryMethods.Pickup,
                deliveryAddress,
                customer,
                model.Basket.Id);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> OrderingCourier(OrderCreateViewModel model)
    {
        if (ModelState.IsValid)
		{
			var deliveryAddress = _mapper.Map<Address>(model.Address);
			var customer = _mapper.Map<Customer>(model.Buyer);

			var order = await _orderService.CreateOrderAsync(
				(PaymentMethods)model.PaymentMethodId,
				DeliveryMethods.Courier,
				deliveryAddress,
				customer,
				model.Basket.Id);
		}

        return RedirectToAction(nameof(Index));
    }

	private string GetAnonymousUserId()
	{
		if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIE))
		{
			return Request.Cookies[Constants.BASKET_COOKIE];
		}
		else
		{
			var userName = Guid.NewGuid().ToString();
			Response.Cookies.Append(Constants.BASKET_COOKIE, userName,
			new CookieOptions
			{
				IsEssential = true,
				Expires = DateTime.Today.AddYears(1)
			});

			return userName;
		}
	}
}
