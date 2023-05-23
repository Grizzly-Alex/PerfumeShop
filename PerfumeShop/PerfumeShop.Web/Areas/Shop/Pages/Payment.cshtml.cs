namespace PerfumeShop.Web.Areas.Shop.Pages;

[Area("Shop")]
[Authorize]
public class PaymentModel : PageModel
{
    private readonly IPaymentService _paymentService;

    public PaymentModel(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }


    [BindProperty]
    public PaymentCardViewModel PaymentCardModel { get; set; } = new();
    public OrderViewModel OrderModel { get; set; }

    public void OnGet()
    {

    }
}
