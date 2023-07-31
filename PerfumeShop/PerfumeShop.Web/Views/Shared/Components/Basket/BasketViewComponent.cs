namespace PerfumeShop.Web.Areas.Shop.Shared.Components.Basket;

[Area("Shop")]
public class BasketViewComponent : ViewComponent
{
	private readonly IBasketQueryService _basketQueryService;
	private readonly SignInManager<AppUser> _signInManager;

	public BasketViewComponent(
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
			ItemsCount = await BasketItemsQtyFromSession()
        };
		return View(viewModel);
	}

	private async Task<int> BasketItemsQtyFromSession()
	{
		if (HttpContext.Session.GetInt32(Constants.BASKET_ITEMS_QTY) is null)
		{
			HttpContext.Session.SetInt32(Constants.BASKET_ITEMS_QTY, await BasketItemsQtyFromDb());
        }

		return HttpContext.Session.GetInt32(Constants.BASKET_ITEMS_QTY).GetValueOrDefault();
    }

	private async Task<int> BasketItemsQtyFromDb()
	{
        return _signInManager.IsSignedIn(HttpContext.User)
			? await _basketQueryService.CountTotalBasketItemsAsync(User.Identity.Name)
			: await _basketQueryService.CountTotalBasketItemsAsync(GetAnnonymousIdFromCookie());
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
