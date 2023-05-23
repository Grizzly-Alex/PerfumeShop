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
    public string CustomerName { get; set; }
	public string CustomerPhone { get; set; }
	public string CustomerEmail { get; set; }
	public string Address { get; set; }
    public decimal ItemsCost { get; private set; }
    public decimal ShippingCost { get; private set; }
    public decimal PromoCodeCost { get; private set; }
    public decimal TotalPrice { get; set; }
}
