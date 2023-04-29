namespace PerfumeShop.Core.Models.Entities;

public sealed class Order : Entity
{
	private readonly List<OrderItem> _orderItems = new();
	public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

	public DateTime OrderDate { get; set; }
	public string? OrderStatus { get; set; }	
	
	public BuyerInfo? BuyerInfo { get; set; }
    public PaymentInfo? PaymentInfo { get; set; }
	public ShippingInfo? ShippingInfo { get; set; }


	public Order()
	{
	}

	public Order(DateTime date, OrderStatuses status, BuyerInfo buyerInfo, PaymentInfo paymentInfo, List<OrderItem> orderItems)
	{
		Guard.Against.Null(buyerInfo, nameof(buyerInfo));
        Guard.Against.Null(paymentInfo, nameof(paymentInfo));
        Guard.Against.Null(orderItems, nameof(orderItems));

        BuyerInfo = buyerInfo;
		PaymentInfo = paymentInfo;
		OrderDate = date;
		OrderStatus = status.GetDisplayName();
        orderItems.ForEach(i => i.OrderId = Id);
		_orderItems = orderItems;
    }
}