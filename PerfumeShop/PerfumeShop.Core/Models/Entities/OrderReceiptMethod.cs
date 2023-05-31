namespace PerfumeShop.Core.Models.Entities;

public sealed class OrderReceiptMethod : Entity
{
	public string Name { get; private set; }

	private OrderReceiptMethod()
	{
	}

	public OrderReceiptMethod(OrderReceiptMethods method)
	{
		Id = (int)method;
		Name = method.GetDisplayName();
	}


	public static implicit operator OrderReceiptMethod(OrderReceiptMethods enumMethods) => new OrderReceiptMethod(enumMethods);
	public static implicit operator OrderReceiptMethods(OrderReceiptMethod classMethod) => (OrderReceiptMethods)classMethod.Id;
}
