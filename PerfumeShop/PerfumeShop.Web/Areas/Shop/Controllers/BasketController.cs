namespace PerfumeShop.Web.Areas.Shop.Controllers;

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
        string userId = await GetBuyerId();
        var basket = await _basketService.GetOrCreateBasketAsync(userId);
        await _basketService.AddItemToBasketAsync(basket.Id, productId, quantity);
        return View();
    }

    private async Task<string> GetBuyerId()
    {
        string? userId = null;

        if (Request.HttpContext.User.Identity.IsAuthenticated)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return user.Id;
        }

        if (Request.Cookies.ContainsKey(Constants.BasketCookie))
        {
            userId = Request.Cookies[Constants.BasketCookie];

            if (!Guid.TryParse(userId, out var _))
            {
                userId = null;
            }
        }

        if (userId is not null) return userId;

        userId = Guid.NewGuid().ToString();

        Response.Cookies.Append(Constants.BasketCookie, userId,
            new CookieOptions
            {
                IsEssential = true,
                Expires = DateTime.Today.AddYears(1)
            });

        return userId;
    }
}
