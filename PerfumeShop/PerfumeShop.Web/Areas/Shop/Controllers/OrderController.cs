namespace PerfumeShop.Web.Areas.Shop.Controllers;

[Area("Shop")]
[Authorize]
public class OrderController : Controller
{
    private readonly IMapper _mapper;
	private readonly IBasketViewModelService _basketViewModelService;
	private readonly IPhysicalShopQueryService _physicalShopQueryService;
    private readonly SignInManager<AppUser> _signInManager;
	private readonly UserManager<AppUser> _userManager;


	public OrderController(
        IMapper mapper,
        IBasketViewModelService basketViewModelService,
		IPhysicalShopQueryService _physicalShopQueryService,
		SignInManager<AppUser> signInManager,
		UserManager<AppUser> userManager)
    {
		_mapper = mapper;
		_basketViewModelService = basketViewModelService;
		_signInManager = signInManager;
		_userManager = userManager;
	}

    [HttpGet]
    public async Task<IActionResult> Index()
    {
		if (_signInManager.IsSignedIn(HttpContext.User))
		{
			var userName = User.Identity.Name;

			var basket = await _basketViewModelService.GetBasketForUserAsync(userName);
			var user = await _userManager.FindByNameAsync(userName);
			var buyer = _mapper.Map<BuyerViewModel>(user);
			var address = _mapper.Map<AddressViewModel>(user);
		}
		else
		{
			var basket = await _basketViewModelService.GetBasketForUserAsync(GetAnonymousUserId());
		}

		var addressShopCollection = await _physicalShopQueryService.GetAllAddresses();	

		return View();
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
