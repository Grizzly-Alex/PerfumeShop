namespace PerfumeShop.Web.Areas.Shop.Controllers;

[Area("Shop")]
//[Authorize]
public class OrderController : Controller
{
	public async Task<IActionResult> Index()
	{
		return View();
	}
}
