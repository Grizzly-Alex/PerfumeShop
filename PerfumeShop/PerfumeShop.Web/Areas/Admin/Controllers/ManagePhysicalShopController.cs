namespace PerfumeShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ManagePhysicalShopController : Controller
{
    private readonly IViewModelService<PhysicalShop, PhysicalShopViewModel, SaleDbContext> _viewModelService;

    public ManagePhysicalShopController(IViewModelService<PhysicalShop, PhysicalShopViewModel, SaleDbContext> viewModelService)
    {
        _viewModelService = viewModelService;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(PhysicalShopViewModel obj)
    {
        if (ModelState.IsValid)
        {
            var model = await _viewModelService.CreateModelAsync(obj);
            TempData["success"] = $"{model.Address.GetFullAddress()} was created successfully";
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
    public async Task<IActionResult> Edit(PhysicalShopViewModel obj)
    {
        if (ModelState.IsValid)
        {
            var model = await _viewModelService.UpdateModelAsync(obj);
            TempData["success"] = $"{model.Address.GetFullAddress()} was updated successfully";
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

        await _viewModelService.DeleteModelAsync(viewModel);

        return Json(new { success = true, message = $"{viewModel.Address.GetFullAddress()} was deleted successfully" });
    }
    #endregion
}
