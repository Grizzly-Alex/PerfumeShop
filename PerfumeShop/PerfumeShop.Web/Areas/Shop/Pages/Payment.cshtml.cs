using PerfumeShop.Web.ViewModels.Order;

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

        var orderHeader = await _orderViewModelService.GetOrderInfoModelAsync(HttpContext.Session.Get<int>(Constants.SESSION_ORDER_ID));
        var test = orderHeader;
    }
}
