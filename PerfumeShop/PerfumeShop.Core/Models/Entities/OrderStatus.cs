namespace PerfumeShop.Core.Models.Entities;

public sealed class OrderStatus : Entity
{
	public string Name { get; private set; }

	private OrderStatus()
	{
	}

	public OrderStatus(OrderStatuses status)
	{
		Id = (int)status;
		Name = status.GetDisplayName();
	}


	public static implicit operator OrderStatus(OrderStatuses enumStatus) => new OrderStatus(enumStatus);
	public static implicit operator OrderStatuses(OrderStatus classStatus) => (OrderStatuses)classStatus.Id;

    public override string ToString() => Name;
}
