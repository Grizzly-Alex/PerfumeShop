namespace PerfumeShop.Web.ViewModels.Order;

public sealed class OrderCreateViewModel
{
	public BasketViewModel Basket { get; set; }
	public BuyerViewModel Buyer { get; set; }
	public AddressViewModel Address { get; set; }
	public int PhysicalShopId { get; set; }
	public List<SelectListItem> PhysicalShopes { get; set; }
	public List<CheckboxItem> PaymentMethos { get; set; }
}
