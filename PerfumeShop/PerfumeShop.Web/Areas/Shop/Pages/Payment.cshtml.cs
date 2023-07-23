namespace PerfumeShop.Web.Areas.Shop.Pages;

[Area("Shop")]
[Authorize]
public class PaymentModel : PageModel
{
    private readonly IPaymentService _paymentService;
    private readonly IOrderQueryService _orderQueryService;
    private readonly IOrderViewModelService _orderViewModelService;
    private readonly IMapper _mapper;

    public PaymentModel(
        IPaymentService paymentService,
        IOrderQueryService orderQueryService,
        IOrderViewModelService orderViewModelService,
        IMapper mapper)
    {
        _paymentService = paymentService;
        _orderQueryService = orderQueryService;
        _orderViewModelService = orderViewModelService;
        _mapper = mapper;
    }

    [BindProperty]
    public PaymentCardViewModel PaymentCardModel { get; set; } = new ();
    [BindProperty]
    public OrderInfoViewModel OrderInfoModel { get; set; } = new();
    public IList<OrderItemViewModel> OrderItemList { get; set; } = new List<OrderItemViewModel>();


    public async Task OnGet()
    {
        if (HttpContext.Session.Keys.Contains(Constants.SESSION_ORDER_TRACKING_ID))
        {
            var orderTrackingId = HttpContext.Session.Get<String>(Constants.SESSION_ORDER_TRACKING_ID)!;
            var orderId = await _orderQueryService.GetOrderIdAsync(orderTrackingId);
            OrderInfoModel = await _orderViewModelService.GetOrderInfoModelAsync(orderId);
            OrderItemList = await _orderViewModelService.GetOrderItemModelCollectionAsync(orderId);
        }
    }


    public async Task<IActionResult> OnPost(CancellationToken ct)
    {
        var paymentCard = _mapper.Map<PaymentCard>(PaymentCardModel);

        var payment = new Payment(
            new Buyer(OrderInfoModel.CustomerEmail, OrderInfoModel.CustomerPhone, OrderInfoModel.CustomerName, paymentCard),
            OrderInfoModel.Id,
            Constants.CURRENCY.ToLower(),
            OrderInfoModel.TotalPrice);

        var paymentDetail = await _paymentService.PayAsync(payment, ct);

        return RedirectToPage("/OrderSuccess");
    }
}
