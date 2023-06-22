namespace PerfumeShop.Web.ViewModels.Order;

public sealed class OrderCreateViewModel
{
	public BasketViewModel Basket { get; set; }
	public BuyerViewModel Buyer { get; set; }
	public AddressViewModel Address { get; set; }

    [Required(ErrorMessage = "Shop must be selected!")]
    public int PhysicalShopId { get; set; }
	public List<SelectListItem> PhysicalShopes { get; set; }

    [Required(ErrorMessage = "Payment method must be selected!")]
    public int PaymentMethodId { get; set; }
    public List<SelectListItem> PaymentMethods { get; set; }
}
