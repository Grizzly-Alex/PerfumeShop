using Microsoft.AspNetCore.Identity;

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

    #region API CALLS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ordersInfo = await _orderViewModelService.GetAllOrderInfoModelAsync();
        return Json(new { data = ordersInfo });
    }
    #endregion


}
