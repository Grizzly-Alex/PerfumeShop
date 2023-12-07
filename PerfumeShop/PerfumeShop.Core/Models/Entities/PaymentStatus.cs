namespace PerfumeShop.Core.Models.Entities;

public sealed class PaymentStatus : Entity
{
	public string Name { get; private set; }

	private PaymentStatus()
	{
	}

	public PaymentStatus(PaymentStatuses status)
	{
		Id = (int)status;
		Name = status.GetDisplayName();
	}


	public static implicit operator PaymentStatus(PaymentStatuses enumStatus) => new PaymentStatus(enumStatus);
	public static implicit operator PaymentStatuses(PaymentStatus classStatus) => (PaymentStatuses)classStatus.Id;

    public override string ToString() => Name;
}
