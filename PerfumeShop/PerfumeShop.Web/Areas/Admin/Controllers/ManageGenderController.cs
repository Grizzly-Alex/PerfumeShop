namespace PerfumeShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ManageGenderController : Controller
{
    private readonly IViewModelService<CatalogGender, CatalogItemViewModel> _viewModelService;

    public ManageGenderController(IViewModelService<CatalogGender, CatalogItemViewModel> viewModelService)
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
            TempData["success"] = $"{obj.Name} was created successfully";
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
            TempData["success"] = $"{obj.Name} was updated successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(obj);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var viewModel = await _viewModelService.GetViewModelByIdAsync(id);
        await _viewModelService.DeleteViewModelAsync(viewModel);
        TempData["success"] = $"{viewModel.Name} was deleted successfully";
        return RedirectToAction(nameof(Index));
    }
}
