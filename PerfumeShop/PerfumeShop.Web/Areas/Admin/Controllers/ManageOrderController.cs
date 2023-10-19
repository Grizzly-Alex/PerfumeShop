namespace PerfumeShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ManageOrderController : Controller
{
    private readonly ILogger<ManageOrderController> _logger;
    private readonly IOrderViewModelService _orderViewModelService;

    public ManageOrderController(
        IOrderViewModelService orderViewModelService,
        ILogger<ManageOrderController> logger)
    {
        _logger = logger;   
        _orderViewModelService = orderViewModelService;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (id == 0)
        {
            return RedirectToAction(nameof(Index));
        }
        else
        {
            var order = await _orderViewModelService.GetOrderViewModelAsync(id);
            return View(order);
        }
    }

    #region API CALLS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ordersInfo = await _orderViewModelService.GetAllOrderInfoModelAsync();
        return Json(new { data = ordersInfo });
    }
    #endregion


}
