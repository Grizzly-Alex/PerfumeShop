namespace PerfumeShop.Web.Areas.Admin.Controllers;

public class ManageCatalogController : Controller
{
    private readonly IViewModelService<CatalogProduct, CatalogProductViewModel> _catalogService;
    private readonly IUnitOfWork<CatalogDbContext> _unitOfWork;

    public ManageCatalogController(
        IUnitOfWork<CatalogDbContext> unitOfWork,
        IViewModelService<CatalogProduct, CatalogProductViewModel> catalogService)      
    {
        _catalogService = catalogService;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _catalogService.GetViewModelsAsync();       
        return View(products);
    }

    //[HttpGet]
    //public async Task<IActionResult> Create()
    //{
    //    var categories = await _unitOfWork.GetRepository<CatalogCategory>().GetAllAsync();
    //    var brands = await _unitOfWork.GetRepository<CatalogBrand>().GetAllAsync();
    //    var genders = await _unitOfWork.GetRepository<CatalogGender>().GetAllAsync();
    //    var releaseForms = await _unitOfWork.GetRepository<CatalogReleaseForm>().GetAllAsync();
    //    var types = await _unitOfWork.GetRepository<CatalogType>().GetAllAsync();

    //}
}
