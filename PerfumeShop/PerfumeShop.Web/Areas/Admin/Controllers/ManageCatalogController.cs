namespace PerfumeShop.Web.Areas.Admin.Controllers;

public class ManageCatalogController : Controller
{
    private readonly IContentManager _contentManager;
    private readonly IViewModelService<CatalogProduct, CatalogProductViewModel> _catalogService;
    private readonly IViewModelService<CatalogBrand, CatalogItemViewModel> _brandService;
    private readonly IViewModelService<CatalogCategory, CatalogCategoryViewModel> _categoryService;
    private readonly IViewModelService<CatalogGender, CatalogGenderViewModel> _genderService;
    private readonly IViewModelService<CatalogType, CatalogTypeViewModel> _typeService;
    private readonly IViewModelService<CatalogReleaseForm, CatalogReleaseFormViewModel> _releaseFormService;


    public ManageCatalogController(
        IContentManager contentManager,
        IViewModelService<CatalogProduct, CatalogProductViewModel> catalogService,
        IViewModelService<CatalogBrand, CatalogItemViewModel> brandService,
        IViewModelService<CatalogCategory, CatalogCategoryViewModel> categoryService,
        IViewModelService<CatalogGender, CatalogGenderViewModel> genderService,
        IViewModelService<CatalogType, CatalogTypeViewModel> typeService,
        IViewModelService<CatalogReleaseForm, CatalogReleaseFormViewModel> releaseFormService)      
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
        var genders = await _genderService.GetViewModelsAsync();
        var releaseForms = await _releaseFormService.GetViewModelsAsync();
        var types = await _typeService.GetViewModelsAsync();

        ManageProductViewModel manageProduct = new(
            new CatalogProductViewModel(),
            )

    }
}
