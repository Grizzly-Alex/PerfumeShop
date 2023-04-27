namespace PerfumeShop.Core.Models.Entities;

public sealed class OrderItem : Entity
{
	public int Quantity { get; set; }
	public decimal Price { get; set; }

    public int ProductId { get; set; }

	public int OrderId { get; set; }
    public Order Order { get; set; }

    private OrderItem()
    {       
    }

    public OrderItem(int quantity, decimal price, int orderId, int productId)
    {
		Guard.Against.OutOfRange(quantity, nameof(quantity), 1, int.MaxValue);
        Guard.Against.NegativeOrZero(price, nameof(price));
		Guard.Against.OutOfRange(orderId, nameof(orderId), 1, int.MaxValue);
		Guard.Against.OutOfRange(productId, nameof(productId), 1, int.MaxValue);

		Quantity = quantity;
        Price = price;
        OrderId = orderId;
        ProductId = productId;
    }
}