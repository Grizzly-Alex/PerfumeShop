using PerfumeShop.Web.ViewModels.Customer;

namespace PerfumeShop.Web.Areas.Shop.Pages;


[Area("Shop")]
[Authorize]
public class CheckoutModel : PageModel
{
	private readonly IMapper _mapper;
    private readonly IBasketViewModelService _basketViewModelService;
	private readonly ICatalogProductService _catalogProductService;
	private readonly IOrderService _orderService;
	private readonly IBasketService _basketService;
	private readonly SignInManager<AppUser> _signInManager;
	private readonly UserManager<AppUser> _userManager;


	public CheckoutModel(
		IMapper mapper,
		IBasketViewModelService basketViewModelService,
		ICatalogProductService catalogProductService,
        IOrderService orderService,
		IBasketService basketService,
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager)
    {
		_mapper = mapper;
		_basketViewModelService = basketViewModelService;
		_catalogProductService = catalogProductService;
        _orderService = orderService;
		_basketService = basketService;
        _signInManager = signInManager;
		_userManager = userManager;
    }

	[BindProperty]
	public CustomerViewModel BuyerInfoModel { get; set; } = new();
    public BasketViewModel BasketModel { get; set; } = new();


	public async Task OnGet()
    {
		await SetModelsAsync();
	}

	public async Task<IActionResult> OnPost(int basketId)
	{
		var addressee = _mapper.Map<Addressee>(BuyerInfoModel);
		var order = await _orderService.CreateOrderAsync(addressee, basketId, BuyerInfoModel.Id);
		await _catalogProductService.UpdateStockAfterOrderAsync(order.OrderItems);
		await _basketService.ClearBasketAsync(basketId);

        return RedirectToPage("OrderSuccess");
    }

	private async Task SetModelsAsync()
	{
		if (_signInManager.IsSignedIn(HttpContext.User))
		{
			var userName = User.Identity.Name;

            BasketModel = await _basketViewModelService.GetBasketForUserAsync(userName);
            var user = await _userManager.FindByNameAsync(userName);
            BuyerInfoModel = _mapper.Map<CustomerViewModel>(user);
        }
		else
		{
			BasketModel = await _basketViewModelService.GetBasketForUserAsync(GetAnonymousUserId());
		}
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