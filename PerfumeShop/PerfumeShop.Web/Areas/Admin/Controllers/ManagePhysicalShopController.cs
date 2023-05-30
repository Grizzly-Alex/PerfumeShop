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
    public IActionResult Create()
    {       
        var viewModel = new ManagePhysicalShopViewModel(CheckBoxHelper.GetCheckBoxList<DayOfWeek>());

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ManagePhysicalShopViewModel manageModel)
    {
        if (ModelState.IsValid)
        {
			manageModel.Shop.Weekends = CheckBoxHelper.GetСheckedItems<DayOfWeek>(manageModel.DayOfWeek);
			var model = await _viewModelService.CreateModelAsync(manageModel.Shop);
            TempData["success"] = $"{model.Address.GetFullAddress()} was created successfully";
            return RedirectToAction(nameof(Index));
        }
        else return View(manageModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var shopViewModel = await _viewModelService.GetViewModelByIdAsync(id);
        var manageModel = new ManagePhysicalShopViewModel(shopViewModel,
            CheckBoxHelper.GetCheckBoxList(shopViewModel.Weekends));

		return View(manageModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ManagePhysicalShopViewModel manageModel)
    {
		if (ModelState.IsValid)
        {
			var model = await _viewModelService.UpdateModelAsync(manageModel.Shop);
            TempData["success"] = $"{model.Address.GetFullAddress()} was updated successfully";
            return RedirectToAction(nameof(Index));
        }
        return View(manageModel);
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
