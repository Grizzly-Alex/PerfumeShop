﻿namespace PerfumeShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ManageProductController : Controller
{
    private readonly IContentManager _contentManager;
    private readonly IViewModelService<CatalogProduct, ProductViewModel, CatalogDbContext> _productService;
    private readonly IViewModelService<CatalogBrand, ItemViewModel, CatalogDbContext> _brandService;
    private readonly IViewModelService<CatalogGender, ItemViewModel, CatalogDbContext> _genderService;
    private readonly IViewModelService<CatalogAromaType, ItemViewModel, CatalogDbContext> _aromaTypeService;
    private readonly IViewModelService<CatalogReleaseForm, ItemViewModel, CatalogDbContext> _releaseFormService;


    public ManageProductController(
        IContentManager contentManager,
        IViewModelService<CatalogProduct, ProductViewModel, CatalogDbContext> catalogService,
        IViewModelService<CatalogBrand, ItemViewModel, CatalogDbContext> brandService,
        IViewModelService<CatalogGender, ItemViewModel, CatalogDbContext> genderService,
        IViewModelService<CatalogAromaType, ItemViewModel, CatalogDbContext> typeService,
        IViewModelService<CatalogReleaseForm, ItemViewModel, CatalogDbContext> releaseFormService)      
    {
        _contentManager = contentManager;
        _productService = catalogService;
        _brandService = brandService;
        _genderService = genderService;
        _aromaTypeService = typeService;
        _releaseFormService = releaseFormService;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var brands = await _brandService.GetViewModelsAsync();
        var types = await _aromaTypeService.GetViewModelsAsync();
        var genders = await _genderService.GetViewModelsAsync();
        var releaseForms = await _releaseFormService.GetViewModelsAsync();

        ManageProductViewModel manageViewModel = new(
            new ProductViewModel(),
            brands.ToSelectListItems(),
            types.ToSelectListItems(),
            genders.ToSelectListItems(),
            releaseForms.ToSelectListItems());

        return View(manageViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ManageProductViewModel manageViewModel)
    {
        var files = HttpContext.Request.Form.Files;

        if (files.Count > 0)
        {
            _contentManager.UploadFiles(HttpContext.Request.Form.Files, Constants.CATALOG_IMAGE_PATH);
            manageViewModel.Product!.PictureUri = _contentManager.NameFiles.FirstOrDefault();
        }

        if (ModelState.IsValid)
        {
            await _productService!.CreateModelAsync(manageViewModel.Product!);

            TempData["success"] = $"{manageViewModel.Product.Name} was created successfully";

            return RedirectToAction(nameof(Index));
        }
        else return View(manageViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService!.GetViewModelByIdAsync(id);
        var brands = await _brandService.GetViewModelsAsync();
        var types = await _aromaTypeService.GetViewModelsAsync();
        var genders = await _genderService.GetViewModelsAsync();
        var releaseForms = await _releaseFormService.GetViewModelsAsync();

        ManageProductViewModel manageViewModel = new(
            product,
            brands.ToSelectListItems(),
            types.ToSelectListItems(),
            genders.ToSelectListItems(),
            releaseForms.ToSelectListItems());

        return View(manageViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ManageProductViewModel manageViewModel)
    {
        var files = HttpContext.Request.Form.Files;

        if (files.Count > 0)
        {
            _contentManager.RemoveFile(Constants.CATALOG_IMAGE_PATH, manageViewModel.Product.PictureUri);
            _contentManager.UploadFiles(files, Constants.CATALOG_IMAGE_PATH);
            manageViewModel.Product.PictureUri = _contentManager.NameFiles.FirstOrDefault();
        }

        if (ModelState.IsValid)
        {
            var viewModel = manageViewModel.Product;
            await _productService!.UpdateModelAsync(viewModel!);

            TempData["success"] = $"{manageViewModel.Product?.Name} was updated successfully";

            return RedirectToAction(nameof(Details), new { id = viewModel!.Id });
        }
        else return RedirectToAction();
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var viewModel = await _productService!.GetViewModelByIdAsync(id);
        return View(viewModel);
    }

    #region API CALLS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var viewModels = await _productService.GetViewModelsAsync();
        return Json(new { data = viewModels });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var viewModel = await _productService.GetViewModelByIdAsync(id);

        if (viewModel is null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }
        _contentManager.RemoveFile(Constants.CATALOG_IMAGE_PATH, viewModel.PictureUri);

        await _productService.DeleteModelAsync(viewModel);

        return Json(new { success = true, message = $"{viewModel.Name} was deleted successfully" });
    }
    #endregion
}
