namespace PerfumeShop.Web.ViewModels.Customer;

public sealed class OrderViewModel
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string TrackingId { get; set; }
    public string OrderStatus { get; set; }
    public string PaymentStatus { get; set; }
    public string PaymentMethod { get; set; }
    public string DeliveryMethod { get; set; }
    public decimal ItemsCost { get; private set; }
    public decimal ShippingCost { get; private set; }
    public decimal PromoCodeCost { get; private set; }
    public decimal TotalPrice { get; set; }
    public BuyerViewModel Buyer { get; set; }
    public AddressViewModel ShippingAddress { get; set; }
}
