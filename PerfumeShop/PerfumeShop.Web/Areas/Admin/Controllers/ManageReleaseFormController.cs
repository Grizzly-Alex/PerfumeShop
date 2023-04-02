﻿namespace PerfumeShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ManageReleaseFormController : Controller
{
    private readonly IViewModelService<CatalogReleaseForm, CatalogItemViewModel> _viewModelService;

    public ManageReleaseFormController(IViewModelService<CatalogReleaseForm, CatalogItemViewModel> viewModelService)
    {
        _viewModelService = viewModelService;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult Create() => View();

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

        return Json(new { success = true, message = $"{viewModel.Name} was deleted successfully" });
    }
    #endregion
}
