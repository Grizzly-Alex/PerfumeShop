using PerfumeShop.Web.ViewModels.Order;

namespace PerfumeShop.Web.Areas.Shop.Controllers;

[Area("Shop")]
[Authorize]
public class OrderController : Controller
{
    private readonly IMapper _mapper;
	private readonly IBasketViewModelService _basketViewModelService;
	private readonly IViewModelService<PhysicalShop, PhysicalShopViewModel, SaleDbContext> _viewModelService;
    private readonly SignInManager<AppUser> _signInManager;
	private readonly UserManager<AppUser> _userManager;


	public OrderController(
        IMapper mapper,
        IBasketViewModelService basketViewModelService,
		IViewModelService<PhysicalShop, PhysicalShopViewModel, SaleDbContext> viewModelService,
		SignInManager<AppUser> signInManager,
		UserManager<AppUser> userManager)
    {
		_mapper = mapper;
		_basketViewModelService = basketViewModelService;
		_viewModelService = viewModelService;
		_signInManager = signInManager;
		_userManager = userManager;
	}

    [HttpGet]
    public async Task<IActionResult> Index()
    {
		var order = new CreateOrderViewModel();

		if (_signInManager.IsSignedIn(HttpContext.User))
		{
			var userName = User.Identity.Name;
			var user = await _userManager.FindByNameAsync(userName);
			order.Basket = await _basketViewModelService.GetBasketForUserAsync(userName);
			order.Buyer = _mapper.Map<BuyerViewModel>(user);
			order.Address = _mapper.Map<AddressViewModel>(user);
		}
		else
		{
			order.Basket = await _basketViewModelService.GetBasketForUserAsync(GetAnonymousUserId());
		}

		var physicalShopes = await _viewModelService.GetViewModelsAsync();
		order.PhysicalShopes = physicalShopes.ToSelectListItems();
		order.PaymentMethos = CheckBoxHelper.GetCheckBoxList<PaymentMethods>();

		return View(order);			
    }

    [HttpPost]
    public async Task<IActionResult> OrderingPickup()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> OrderingCourier()
    {
        return View();
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
