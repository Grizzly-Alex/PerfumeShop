namespace PerfumeShop.Web.Areas.Admin.Controllers;

public class ManageReleaseFormController : Controller
{
    private readonly IViewModelService<CatalogReleaseForm, CatalogItemViewModel> _viewModelService;

    public ManageReleaseFormController(IViewModelService<CatalogReleaseForm, CatalogItemViewModel> viewModelService)
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


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var viewModel = await _viewModelService.GetViewModelByIdAsync(id);
        await _viewModelService.DeleteViewModelAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }
}
