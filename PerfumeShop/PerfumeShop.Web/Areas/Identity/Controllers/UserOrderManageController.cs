namespace PerfumeShop.Web.Areas.Identity.Controllers;

[Authorize]
[Area("Identity")]
[Route("[controller]/[action]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class UserOrderManageController : Controller
{
    private readonly IMapper _mapper;
    private readonly IOrderViewModelService _orderViewModelService;
    private readonly UserManager<AppUser> _userManager;

    public UserOrderManageController(
        IMapper mapper,
        IOrderViewModelService orderViewModelService,
        UserManager<AppUser> userManager)
    {
        _mapper = mapper;
        _orderViewModelService = orderViewModelService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var ordersInfo = await _orderViewModelService.GetOrderInfoModelCollectionAsync(_userManager.GetUserId(User));
        return View(ordersInfo);
    }
}
