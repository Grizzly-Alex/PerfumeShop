namespace PerfumeShop.Web.Areas.User.Controllers;


[Area("User")]
public class CatalogController : Controller
{
	private readonly ICatalogService _catalogService;
	private readonly IViewModelService<CatalogProduct, ProductViewModel> _viewModelService;

    public CatalogController(
		ICatalogService catalogService,
		IViewModelService<CatalogProduct, ProductViewModel> viewModelService)
    {
        _catalogService = catalogService;
		_viewModelService = viewModelService;
    }

	[HttpGet]
	public async Task<IActionResult> Index(CatalogFilterViewModel filter, PagedInfoViewModel pagedInfo)
	{
		filter.MaxPrice = await _catalogService.DefineMaxPrice(filter.MaxPrice);
		var pagedList = await _catalogService.GetCatalogPagedListAsync(filter, 8, pagedInfo.PageId);
		var productFilter = await _catalogService.GetCatalogFilterAsync(filter.MinPrice, filter.MaxPrice);
		var catalog = new CatalogViewModel(pagedList, productFilter);

		return View(catalog);
	}

	[HttpGet]
	public async Task<IActionResult> Details(int id)
	{
		var guitarViewModel = await _viewModelService.GetViewModelByIdAsync(id);

		return View(guitarViewModel);
	}
}
