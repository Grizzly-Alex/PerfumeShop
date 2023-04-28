namespace PerfumeShop.Core.Models.Entities;

public sealed class Order : Entity
{
	private readonly List<OrderItem> _orderItems = new();
	public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

	public DateTime OrderDate { get; set; }
	public decimal OrderTotal { get; set; }
	public string? OrderStatus { get; set; }	
	
	public BuyerInfo BuyerInfo { get; set; }
    public PaymentInfo PaymentInfo { get; set; }
	public ShippingInfo ShippingInfo { get; set; }


	private Order()
	{
	}

	public Order(
		BuyerInfo buyerInfo,
		ShippingInfo shippingInfo,
		PaymentInfo paymentInfo,
		List<OrderItem> items)
	{
        Guard.Against.Null(buyerInfo, nameof(buyerInfo));
        Guard.Against.Null(shippingInfo, nameof(shippingInfo));
        Guard.Against.Null(paymentInfo, nameof(paymentInfo));
		Guard.Against.Null(items, nameof(items));

        BuyerInfo = buyerInfo;
		ShippingInfo = shippingInfo;
		PaymentInfo = paymentInfo;
		_orderItems = items;
	}
}