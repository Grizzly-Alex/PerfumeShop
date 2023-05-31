namespace PerfumeShop.Web.Areas.Shop.Shared.Components.Basket;

[Area("Shop")]
public class Basket : ViewComponent
{
	private readonly IBasketQueryService _basketQueryService;
	private readonly SignInManager<AppUser> _signInManager;

	public Basket(
		IBasketQueryService basketQueryService,
		SignInManager<AppUser> signInManager)
    {
        _basketQueryService = basketQueryService;
		_signInManager = signInManager;
    }

	public async Task<IViewComponentResult> InvokeAsync()
	{
		var viewModel = new BasketComponentViewModel
		{
			ItemsCount = await CountTotalBasketItems()
		};
		return View(viewModel);
	}

	private async Task<int> CountTotalBasketItems()
	{
		if (_signInManager.IsSignedIn(HttpContext.User))
		{
			return await _basketQueryService.CountTotalBasketItemsAsync(User.Identity.Name);
		}

		string anonymousId = GetAnnonymousIdFromCookie();

		if (anonymousId == null) return 0;

		return await _basketQueryService.CountTotalBasketItemsAsync(anonymousId);
	}

	private string GetAnnonymousIdFromCookie()
	{
		if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIE))
		{
			var id = Request.Cookies[Constants.BASKET_COOKIE];

			if (Guid.TryParse(id, out var _))
			{
				return id;
			}
		}
		return null;
	}
}
