namespace PerfumeShop.Web.Areas.Shop.Controllers;

[Area("Shop")]
public class OrderController : Controller
{
    private readonly IMapper _mapper;

    public OrderController()
    {
        
    }

    [HttpGet]
    public async Task<IActionResult> FormPickup()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> FormCourier()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> OrderingPickup()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> OrderingCourier()
    {
        return View();
    }

}
