namespace PerfumeShop.Web.Areas.Admin.Controllers;

public class ManageCatalogController : Controller
{
    private readonly IContentManager _contentManager;
    private readonly IViewModelService<CatalogProduct, CatalogProductViewModel> _catalogService;
    private readonly IViewModelService<CatalogBrand, CatalogItemViewModel> _brandService;
    private readonly IViewModelService<CatalogCategory, CatalogItemViewModel> _categoryService;
    private readonly IViewModelService<CatalogGender, CatalogItemViewModel> _genderService;
    private readonly IViewModelService<CatalogType, CatalogItemViewModel> _typeService;
    private readonly IViewModelService<CatalogReleaseForm, CatalogItemViewModel> _releaseFormService;


    public ManageCatalogController(
        IContentManager contentManager,
        IViewModelService<CatalogProduct, CatalogProductViewModel> catalogService,
        IViewModelService<CatalogBrand, CatalogItemViewModel> brandService,
        IViewModelService<CatalogCategory, CatalogItemViewModel> categoryService,
        IViewModelService<CatalogGender, CatalogItemViewModel> genderService,
        IViewModelService<CatalogType, CatalogItemViewModel> typeService,
        IViewModelService<CatalogReleaseForm, CatalogItemViewModel> releaseFormService)      
    {
        _contentManager = contentManager;
        _catalogService = catalogService;
        _brandService = brandService;
        _categoryService = categoryService;
        _genderService = genderService;
        _typeService = typeService;
        _releaseFormService = releaseFormService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _catalogService.GetViewModelsAsync();       
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetViewModelsAsync();
        var brands = await _brandService.GetViewModelsAsync();
        var types = await _typeService.GetViewModelsAsync();
        var genders = await _genderService.GetViewModelsAsync();
        var releaseForms = await _releaseFormService.GetViewModelsAsync();

        ManageProductViewModel manageProduct = new(
            new CatalogProductViewModel(),
            categories.ToSelectListItems(),
            brands.ToSelectListItems(),
            types.ToSelectListItems(),
            genders.ToSelectListItems(),
            releaseForms.ToSelectListItems());

        return View(manageProduct);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ManageProductViewModel manageProductViewModel)
    {
        var files = HttpContext.Request.Form.Files;

        if (files.Count > 0)
        {
            _contentManager.UploadFiles(HttpContext.Request.Form.Files, Constants.CatalogImagePath);
            manageProductViewModel.ProductViewModel!.PictureUri = _contentManager.NameFiles.FirstOrDefault();
        }

        if (ModelState.IsValid)
        {
            await _catalogService!.CreateViewModelAsync(manageProductViewModel.ProductViewModel!);
            return RedirectToAction(nameof(Index));
        }
        else return View(manageProductViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _catalogService!.GetViewModelByIdAsync(id);
        var categories = await _categoryService.GetViewModelsAsync();
        var brands = await _brandService.GetViewModelsAsync();
        var types = await _typeService.GetViewModelsAsync();
        var genders = await _genderService.GetViewModelsAsync();
        var releaseForms = await _releaseFormService.GetViewModelsAsync();

        ManageProductViewModel manageProductViewModel = new(
            product,
            categories.ToSelectListItems(),
            brands.ToSelectListItems(),
            types.ToSelectListItems(),
            genders.ToSelectListItems(),
            releaseForms.ToSelectListItems());

        return View(manageProductViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ManageProductViewModel manageProductViewModel)
    {
        var files = HttpContext.Request.Form.Files;

        if (files.Count > 0)
        {
            _contentManager.RemoveFile(Constants.CatalogImagePath, manageProductViewModel.ProductViewModel.PictureUri);
            _contentManager.UploadFiles(files, Constants.CatalogImagePath);
            manageProductViewModel.ProductViewModel.PictureUri = _contentManager.NameFiles.FirstOrDefault();
        }

        if (ModelState.IsValid)
        {
            var viewModel = manageProductViewModel.ProductViewModel;
            await _catalogService!.UpdateViewModelAsync(viewModel!);

            return RedirectToAction(nameof(Details), new { id = viewModel!.Id });
        }
        else return RedirectToAction();
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var productViewModel = await _catalogService!.GetViewModelByIdAsync(id);

        _contentManager.RemoveFile(Constants.CatalogImagePath, productViewModel.PictureUri);

        await _catalogService.DeleteViewModelAsync(productViewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var productViewModel = await _catalogService!.GetViewModelByIdAsync(id);
        return View(productViewModel);
    }
}
