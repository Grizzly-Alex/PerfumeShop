namespace PerfumeShop.Core.Models.Entities;

public sealed class PaymentMethod : Entity
{
	public string Name { get; private set; }

	private PaymentMethod()
	{
	}

	public PaymentMethod(PaymentMethods method)
	{
		Id = (int)method;
		Name = method.GetDisplayName();
	}


	public static implicit operator PaymentMethod(PaymentMethods enumMethods) => new PaymentMethod(enumMethods);
	public static implicit operator PaymentMethods(PaymentMethod classMethod) => (PaymentMethods)classMethod.Id;
}
