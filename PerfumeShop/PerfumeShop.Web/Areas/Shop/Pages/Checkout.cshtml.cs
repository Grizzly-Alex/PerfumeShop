namespace PerfumeShop.Web.Areas.Shop.Pages;


[Area("Shop")]
//[Authorize]
public class CheckoutModel : PageModel
{
    private readonly IBasketViewModelService _basketViewModelService;
	private readonly IMapper _mapper;
	private readonly SignInManager<AppUser> _signInManager;
	private readonly UserManager<AppUser> _userManager;


	public CheckoutModel(
		IBasketViewModelService basketViewModelService,
		IMapper mapper,
		SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager)
    {
		_basketViewModelService = basketViewModelService;
		_mapper = mapper;
		_signInManager = signInManager;
		_userManager = userManager;
    }

	public BasketViewModel BasketModel { get; set; } = new();
	public BuyerInfoViewModel BuyerInfoModel { get; set; } = new();

	public async Task OnGet()
    {
		await SetByerInfoModelAsync();
		await SetBasketModelAsync();
	}

	private async Task SetBasketModelAsync()
	{
		if (_signInManager.IsSignedIn(HttpContext.User))
		{
			BasketModel = await _basketViewModelService.GetBasketForUserAsync(User.Identity.Name);
		}
		else
		{
			BasketModel = await _basketViewModelService.GetBasketForUserAsync(GetAnonymousUserId());
		}
	}

	private async Task SetByerInfoModelAsync()
	{
		if (!_signInManager.IsSignedIn(HttpContext.User)) return;	
		
		var user = await _userManager.FindByNameAsync(User.Identity.Name);
		BuyerInfoModel = _mapper.Map<BuyerInfoViewModel>(user);
    }

	private string GetAnonymousUserId()
	{		
		if (Request.Cookies.ContainsKey(Constants.BasketCookie))
		{
			return Request.Cookies[Constants.BasketCookie];
		}
		else
		{
			var userName = Guid.NewGuid().ToString();
			Response.Cookies.Append(Constants.BasketCookie, userName,
			new CookieOptions
			{
				IsEssential = true,
				Expires = DateTime.Today.AddYears(1)
			});

			return userName;
		}		
	}
}