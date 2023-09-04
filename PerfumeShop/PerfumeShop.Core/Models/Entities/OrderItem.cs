namespace PerfumeShop.Core.Models.Entities;

public sealed class OrderItem : Entity
{
	public int Quantity { get; set; }
	public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public int ProductId { get; set; }
	public int OrderId { get; set; }
    public OrderHeader Order { get; set; }

    private OrderItem()
    {       
    }

    public OrderItem(int quantity, decimal price, int productId)
    {
		Quantity = Guard.Against.OutOfRange(quantity, nameof(quantity), 1, int.MaxValue); 
        Price = Guard.Against.NegativeOrZero(price, nameof(price)); 
        ProductId = Guard.Against.OutOfRange(productId, nameof(productId), 1, int.MaxValue); 
        TotalPrice = price * quantity;
    }
}