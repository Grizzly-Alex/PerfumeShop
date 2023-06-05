namespace PerfumeShop.Web.ViewModels.Order;

public sealed class CreateOrderViewModel
{
	public BasketViewModel Basket { get; set; }
	public BuyerViewModel Buyer { get; set; }
	public AddressViewModel Address { get; set; }
	public List<SelectListItem> PhysicalShopes { get; set; }
	public List<CheckboxItem> PaymentMethos { get; set; }
}
