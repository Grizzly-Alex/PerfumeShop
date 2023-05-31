namespace PerfumeShop.Web.Areas.Shop.Pages;

[Area("Shop")]
[Authorize]
public class PaymentModel : PageModel
{
    private readonly IPaymentService _paymentService;
    private readonly IOrderViewModelService _orderViewModelService;

    public PaymentModel(
        IPaymentService paymentService,
        IOrderViewModelService orderViewModelService)
    {
        _paymentService = paymentService;
        _orderViewModelService = orderViewModelService;
    }


    [BindProperty]
    public PaymentCardViewModel PaymentCardModel { get; set; } = new();
    public OrderInfoViewModel OrderModel { get; set; }

    public async Task OnGet()
    {

        var orderHeader = await _orderViewModelService.GetOrderAsync(HttpContext.Session.Get<int>(Constants.SessionOrderId));
        var test = orderHeader;



    }
}
