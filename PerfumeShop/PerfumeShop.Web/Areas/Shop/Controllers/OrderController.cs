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
	private readonly ICatalogProductService _catalogProductService;
	private readonly IBasketService _basketService;
	private readonly SignInManager<AppUser> _signInManager;
	private readonly UserManager<AppUser> _userManager;


	public OrderController(
		IMapper mapper,
		IBasketViewModelService basketViewModelService,
		IOrderService orderService,
		IOrderViewModelService orderViewModelService,
        IPhysicalShopQueryService physicalShopQueryService,
		ICatalogProductService catalogProductService,
		IBasketService basketService,
		SignInManager<AppUser> signInManager,
		UserManager<AppUser> userManager)
    {
		_mapper = mapper;
		_basketViewModelService = basketViewModelService;
		_orderService = orderService;
		_orderViewModelService = orderViewModelService;
		_physicalShopQueryService = physicalShopQueryService;
		_catalogProductService = catalogProductService;
		_basketService = basketService;
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
    public async Task<IActionResult> OrderByPickup(OrderCreateViewModel model)
    {
		ModelState.Remove(nameof(model.Address));

        if (ModelState.IsValid)
        {
			var deliveryAddress = await _physicalShopQueryService.GetAddressAsync(model.PickupPointId);
            var customer = _mapper.Map<Customer>(model.Buyer);
            var paymentMethod = (PaymentMethods)model.PaymentMethodId;

            var order = await _orderService.CreateOrderAsync(
                paymentMethod,
                DeliveryMethods.Pickup,
                deliveryAddress,
                customer,
                model.Basket.Id);

            SaveTrackingIdToSession(order.TrackingId);

            await _catalogProductService.UpdateStockAfterOrderAsync(order.OrderItems);
			await _basketService.ClearBasketAsync(model.Basket.Id);

            HttpContext.Session.Remove(Constants.BASKET_ITEMS_QTY);

            return RedirectToPage(GetRedirectionPageName(paymentMethod));
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> OrderByCourier(OrderCreateViewModel model)
    {
        if (ModelState.IsValid)
		{
			var deliveryAddress = _mapper.Map<Address>(model.Address);
			var customer = _mapper.Map<Customer>(model.Buyer);
			var paymentMethod = (PaymentMethods)model.PaymentMethodId;

			var order = await _orderService.CreateOrderAsync(
                paymentMethod,
				DeliveryMethods.Courier,
				deliveryAddress,
				customer,
				model.Basket.Id);

			SaveTrackingIdToSession(order.TrackingId);

            await _catalogProductService.UpdateStockAfterOrderAsync(order.OrderItems);
			await _basketService.ClearBasketAsync(model.Basket.Id);

            HttpContext.Session.Remove(Constants.BASKET_ITEMS_QTY);

            return RedirectToPage(GetRedirectionPageName(paymentMethod));
		}

        return RedirectToAction(nameof(Index));
    }

	private void SaveTrackingIdToSession(string trackingId)
	{
        if (HttpContext.Session.Keys.Contains(Constants.SESSION_ORDER_TRACKING_ID))
        {
            HttpContext.Session.Remove(Constants.SESSION_ORDER_TRACKING_ID);
        }
        HttpContext.Session.Set<String>(Constants.SESSION_ORDER_TRACKING_ID, trackingId);
    }

	private string GetRedirectionPageName(PaymentMethods method) => method switch
	{
		PaymentMethods.Cash => "/OrderSuccess",
		PaymentMethods.PaymentCard => "/Payment",
		_ => throw new ArgumentOutOfRangeException(nameof(method), $"Not expected direction value: {method}"),
    };

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
