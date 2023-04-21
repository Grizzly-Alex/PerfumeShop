namespace PerfumeShop.Web.Areas.Shop.Controllers;

[Area("Shop")]
public class BasketController : Controller
{
    private readonly IBasketService _basketService;
    private readonly UserManager<AppUser> _userManager;

    public BasketController(
        IBasketService basketService,
        UserManager<AppUser> userManager)
    {
        _basketService = basketService; 
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddToBasket(int productId, int quantity = 1)
    {
        var userName = GetBuyerId();

        bool isAvailable = await _basketService.IsStockQtyAvailable(userName, productId, quantity);
        if (!isAvailable)
        {
            return Redirect(Request.GetTypedHeaders().Referer.ToString());
        }

        await _basketService.AddItemToBasketAsync(userName, productId, quantity);

        return Redirect(Request.GetTypedHeaders().Referer.ToString());
    }

    private string GetBuyerId()
    {
        string? userName = null;

        if (Request.HttpContext.User.Identity.IsAuthenticated)
        {
            return Request.HttpContext.User.Identity.Name;
        }
        if (Request.Cookies.ContainsKey(Constants.BasketCookie))
        {
            userName = Request.Cookies[Constants.BasketCookie];

            if (!Request.HttpContext.User.Identity.IsAuthenticated 
                && !Guid.TryParse(userName, out var _))
            {
                userName = null;                
            }
        }
        if (userName != null) return userName;

        userName = Guid.NewGuid().ToString();

        Response.Cookies.Append(Constants.BasketCookie, userName,
            new CookieOptions 
            {
                IsEssential = true,
                Expires = DateTime.Today.AddYears(1)
            });

        return userName;
    }
}
