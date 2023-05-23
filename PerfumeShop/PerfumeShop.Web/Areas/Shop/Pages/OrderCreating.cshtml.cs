namespace PerfumeShop.Web.Areas.Shop.Pages;


[Area("Shop")]
[Authorize]
public class OrderCreatingModel : PageModel
{
	private readonly IMapper _mapper;
    private readonly IBasketViewModelService _basketViewModelService;
	private readonly ICatalogProductService _catalogProductService;
	private readonly IOrderService _orderService;
	private readonly IBasketService _basketService;
	private readonly IPaymentService _paymentService;
	private readonly SignInManager<AppUser> _signInManager;
	private readonly UserManager<AppUser> _userManager;


	public OrderCreatingModel(
		IMapper mapper,
		IBasketViewModelService basketViewModelService,
		ICatalogProductService catalogProductService,
        IOrderService orderService,
		IBasketService basketService,
		IPaymentService paymentService,
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager)
    {
		_mapper = mapper;
		_basketViewModelService = basketViewModelService;
		_catalogProductService = catalogProductService;
        _orderService = orderService;
		_basketService = basketService;
		_paymentService = paymentService;
        _signInManager = signInManager;
		_userManager = userManager;
    }

    public BasketViewModel BasketModel { get; set; } = new();
    [BindProperty]
    public PaymentCardViewModel PaymentCardModel { get; set; } = new();
	[BindProperty]
	public BuyerViewModel BuyerModel { get; set; } = new();
	[BindProperty]
	public AddressViewModel AddressModel { get; set; } = new();
    [BindProperty]
    public PaymentMethods PaymentMethod { get; set; }
    [BindProperty]
    public DeliveryMethods DeliveryMethod { get; set; }


	public async Task OnGet()
    {
		await SetModelsAsync();
	}

	public async Task<IActionResult> OnPost(int basketId, CancellationToken ct)
	{
		if (!ModelState.IsValid) return Page();

        var shippingAddress = _mapper.Map<Address>(AddressModel);
        var customer = _mapper.Map<Customer>(BuyerModel);
        		
        var order = await _orderService.CreateOrderAsync(shippingAddress, customer, basketId);
		HttpContext.Session.Set(Constants.SessionOrderHeader, _mapper.Map<OrderViewModel>(order));
		
		//var paymentCard = _mapper.Map<PaymentCard>(PaymentCardModel);
		/*
        Buyer buyer = new(customer.ReceiptEmail, customer.PhoneNumber, customer.GetFullName(), shippingAddress, paymentCard);
		Payment payment = new(buyer, order.Id, Constants.Currency.ToLower(), order.Cost.TotalCost);	
		
		var paymentDetail = await _paymentService.PayAsync(payment, ct);*/

		await _catalogProductService.UpdateStockAfterOrderAsync(order.OrderItems);
		await _basketService.ClearBasketAsync(basketId);

        return PaymentMethod switch
        {
            PaymentMethods.PaymentPlace => RedirectToPage("OrderSuccess"),
            PaymentMethods.PaymentRemote => RedirectToPage("Payment"),
            _ => Page(),
        };
    }

    private async Task SetModelsAsync()
	{
		if (_signInManager.IsSignedIn(HttpContext.User))
		{
			var userName = User.Identity.Name;

            BasketModel = await _basketViewModelService.GetBasketForUserAsync(userName);
            var user = await _userManager.FindByNameAsync(userName);
            BuyerModel = _mapper.Map<BuyerViewModel>(user);
			AddressModel = _mapper.Map<AddressViewModel>(user);
        }
		else
		{
			BasketModel = await _basketViewModelService.GetBasketForUserAsync(GetAnonymousUserId());
		}
    }

	private string GetAnonymousUserId()
	{		
		if (Request.Cookies.ContainsKey(Constants.BasketCookie))
		{
			return Request.Cookies[Constants.BasketCookie];
		}
		else
		{
			var userName = Guid.NewGuid().ToString();
			Response.Cookies.Append(Constants.BasketCookie, userName,
			new CookieOptions
			{
				IsEssential = true,
				Expires = DateTime.Today.AddYears(1)
			});

			return userName;
		}		
	}
}