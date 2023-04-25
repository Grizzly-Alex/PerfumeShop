namespace PerfumeShop.Web.Areas.Shop.Components.Basket;

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
		if (anonymousId == null)
			return 0;

		return await _basketQueryService.CountTotalBasketItemsAsync(anonymousId);
	}

	private string GetAnnonymousIdFromCookie()
	{
		if (Request.Cookies.ContainsKey(Constants.BasketCookie))
		{
			var id = Request.Cookies[Constants.BasketCookie];

			if (Guid.TryParse(id, out var _))
			{
				return id;
			}
		}
		return null;
	}
}
