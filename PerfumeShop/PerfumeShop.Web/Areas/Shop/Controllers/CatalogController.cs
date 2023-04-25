namespace PerfumeShop.Web.Areas.User.Controllers;

[Area("Shop")]
[AllowAnonymous]
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


	public IActionResult Reset(int pageSize) 
		=> RedirectToAction(nameof(Index), new {itemsPerPage = pageSize});

	[HttpGet]
	public async Task<IActionResult> Index(
        PagedInfoViewModel pageInfo,
        decimal? minPrice, decimal? maxPrice,
        int? brandId, int? genderId, int? aromaTypeId, int? releaseFormId)
	{
        maxPrice = await _catalogService.DefineMaxPrice(maxPrice);
        
		var pagedList = await _catalogService.GetCatalogPagedListAsync(
            pageInfo.ItemsPerPage == default ? (int)ItemsPerPage.Ten : pageInfo.ItemsPerPage,
            pageInfo.PageIndex,
			minPrice, maxPrice,
			brandId, genderId, aromaTypeId, releaseFormId);

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
