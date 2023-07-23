namespace PerfumeShop.Web.ViewModels.Order;

public sealed class OrderCreateViewModel
{
	public BasketViewModel Basket { get; set; }
	public BuyerViewModel Buyer { get; set; }
	public AddressViewModel Address { get; set; }

    [Required(ErrorMessage = "Pickup point must be selected!")]
    public int PickupPointId { get; set; }
    [ValidateNever]
    public List<SelectListItem> PickupPoints { get; set; }

    [Required(ErrorMessage = "Payment method must be selected!")]
    public int PaymentMethodId { get; set; }
    [ValidateNever]
    public List<SelectListItem> PaymentMethods { get; set; }
}
