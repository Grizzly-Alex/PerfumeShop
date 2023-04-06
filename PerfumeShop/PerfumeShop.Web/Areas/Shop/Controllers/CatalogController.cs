namespace PerfumeShop.Web.Areas.User.Controllers;


[Area("Shop")]
public class CatalogController : Controller
{
	private readonly ICatalogViewModelService _catalogService;
	private readonly IViewModelService<CatalogProduct, ProductViewModel> _viewModelService;

    public CatalogController(
		ICatalogViewModelService catalogService,
		IViewModelService<CatalogProduct, ProductViewModel> viewModelService)
    {
        _catalogService = catalogService;
		_viewModelService = viewModelService;
    }


	public IActionResult Reset() => RedirectToAction(nameof(Index));

	[HttpGet]
	public async Task<IActionResult> Index(
		int pageIndex,
        decimal? minPrice, decimal? maxPrice,
        int? brandId, int? genderId, int? aromaTypeId, int? releaseFormId)
	{
        maxPrice = await _catalogService.DefineMaxPrice(maxPrice);

		var pagedList = await _catalogService.GetCatalogPagedListAsync(
			Constants.ItemsPerPage, pageIndex, minPrice, maxPrice, brandId, genderId, aromaTypeId, releaseFormId);

		var catalogIndex = await _catalogService.GetCatalogIndexAsync(pagedList, minPrice, maxPrice);

		return View(catalogIndex);
	}

	[HttpGet]
	public async Task<IActionResult> Details(int id)
	{
		var guitarViewModel = await _viewModelService.GetViewModelByIdAsync(id);

		return View(guitarViewModel);
	}
}
