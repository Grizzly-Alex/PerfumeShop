namespace PerfumeShop.Core.Models.Entities;

public sealed class Order : Entity
{
	private readonly List<OrderItem> _orderItems = new();
	public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

	public DateTime OrderDate { get; set; }
	public decimal OrderTotal { get; set; }
	public string? OrderStatus { get; set; }	
	
	public PaymentInfo PaymentInfo { get; set; }
	public ShippingInfo ShippingInfo { get; set; }

	public string BuyerId { get; set; }

	private Order()
	{
	}

	public Order(string buyerId, ShippingInfo shippingInfo, PaymentInfo paymentInfo, List<OrderItem> items)
	{
		Guard.Against.NullOrEmpty(buyerId, nameof(buyerId));
		Guard.Against.Null(shippingInfo, nameof(shippingInfo));
		Guard.Against.Null(items, nameof(items));

		BuyerId = buyerId;
		ShippingInfo = shippingInfo;
		PaymentInfo = paymentInfo;
		_orderItems = items;
	}
}
