namespace PerfumeShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ManageBrandController : Controller
{
    private readonly IViewModelService<CatalogBrand, CatalogItemViewModel> _viewModelService;

    public ManageBrandController(IViewModelService<CatalogBrand, CatalogItemViewModel> viewModelService)
    {
        _viewModelService = viewModelService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewModels = await _viewModelService.GetViewModelsAsync();
        return View(viewModels);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CatalogItemViewModel obj)
    {
        if (ModelState.IsValid)
        {
            await _viewModelService.CreateViewModelAsync(obj);
            return RedirectToAction(nameof(Index));
        }
        else return View(obj);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var viewModel = await _viewModelService.GetViewModelByIdAsync(id);
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CatalogItemViewModel obj)
    {
        if (ModelState.IsValid)
        {
            await _viewModelService.UpdateViewModelAsync(obj);
            return RedirectToAction(nameof(Index));
        }
        return View(obj);
    }

    #region API CALLS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
		var viewModels = await _viewModelService.GetViewModelsAsync();
        return Json(new { data = viewModels });
	}

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var viewModel = await _viewModelService.GetViewModelByIdAsync(id);

        if (viewModel is null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        await _viewModelService.DeleteViewModelAsync(viewModel);

        return Json(new { success = true, message = "Delete Successful" });
    }
    #endregion
}
