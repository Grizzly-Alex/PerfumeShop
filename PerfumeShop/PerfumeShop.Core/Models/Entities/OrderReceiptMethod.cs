namespace PerfumeShop.Core.Models.Entities;

public sealed class OrderDeliveryMethod : Entity
{
	public string Name { get; private set; }

	private OrderDeliveryMethod()
	{
	}

	public OrderDeliveryMethod(OrderDeliveryMethods method)
	{
		Id = (int)method;
		Name = method.GetDisplayName();
	}


	public static implicit operator OrderDeliveryMethod(OrderDeliveryMethods enumMethods) => new OrderDeliveryMethod(enumMethods);
	public static implicit operator OrderDeliveryMethods(OrderDeliveryMethod classMethod) => (OrderDeliveryMethods)classMethod.Id;
}
