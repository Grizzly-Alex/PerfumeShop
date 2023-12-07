namespace PerfumeShop.Web.Areas.User.Controllers;

[Area("Shop")]
[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICatalogViewModelService _catalogViewModelService;

    public HomeController(
        ILogger<HomeController> logger,
        ICatalogViewModelService catalogViewModelService)
    {
        _logger = logger;
        _catalogViewModelService = catalogViewModelService;
    }

    public async Task<IActionResult> Index()
    {
        var productsView = await _catalogViewModelService.GetAllDiscountedProducts(onlyAvailable: true);

        return View(productsView);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogError("Error");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}