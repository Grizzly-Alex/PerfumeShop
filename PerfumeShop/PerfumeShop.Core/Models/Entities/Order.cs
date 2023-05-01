namespace PerfumeShop.Core.Models.Entities;

public sealed class Order : Entity
{
	private readonly List<OrderItem> _orderItems = new();
	public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

	public DateTime OrderDate { get; set; }

	public BuyerInfo? BuyerInfo { get; set; }
    public PaymentInfo? PaymentInfo { get; set; }
	public ShippingInfo? ShippingInfo { get; set; }

	public int OrderStatusId { get; set; }
	public OrderStatus OrderStatus { get; set; }


	public Order()
	{
	}

	public Order(DateTime date, OrderStatuses status, BuyerInfo buyerInfo, PaymentInfo paymentInfo, List<OrderItem> orderItems)
	{
		OrderDate = date;
        orderItems.ForEach(i => i.OrderId = Id);
		_orderItems = Guard.Against.Null(orderItems, nameof(orderItems));
        BuyerInfo = Guard.Against.Null(buyerInfo, nameof(buyerInfo));
		PaymentInfo = Guard.Against.Null(paymentInfo, nameof(paymentInfo));
		OrderStatusId = (int)status;
	}
}