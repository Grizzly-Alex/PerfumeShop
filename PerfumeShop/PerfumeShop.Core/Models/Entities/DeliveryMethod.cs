namespace PerfumeShop.Core.Models.Entities;

public sealed class DeliveryMethod : Entity
{
	public string Name { get; private set; }

	private DeliveryMethod()
	{
	}

	public DeliveryMethod(DeliveryMethods method)
	{
		Id = (int)method;
		Name = method.GetDisplayName();
	}


	public static implicit operator DeliveryMethod(DeliveryMethods enumMethods) => new DeliveryMethod(enumMethods);
	public static implicit operator DeliveryMethods(DeliveryMethod classMethod) => (DeliveryMethods)classMethod.Id;

	public override string ToString() => Name;
}
