namespace PerfumeShop.Core.Models.Entities;

public sealed class OrderDetail : Entity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public Cost Cost { get; private set; }
    public Addressee Addressee { get; private set; }

	public OrderDetail()
	{
	}

	public OrderDetail(
        int orderId,
        Cost cost,
        Addressee addressee)
    {
        OrderId = Guard.Against.NegativeOrZero(orderId, nameof(orderId));
        Addressee = Guard.Against.Null(addressee, nameof(addressee));
        Cost = Guard.Against.Null(cost, nameof(cost));
    } 
}