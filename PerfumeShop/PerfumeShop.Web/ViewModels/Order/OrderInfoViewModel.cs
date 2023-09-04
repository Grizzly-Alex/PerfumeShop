namespace PerfumeShop.Web.ViewModels.Order;

public sealed class OrderInfoViewModel
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
    public decimal TotalPrice { get; private set; }
    public string DateFormat => OrderDate.ToString("MM/dd/yyyy hh:mm tt");
    public string ItemsCostFormat => ItemsCost.ToString("c");
    public string TotalPriceFormat => TotalPrice.ToString("c");
}
