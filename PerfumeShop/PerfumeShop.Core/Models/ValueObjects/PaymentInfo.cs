namespace PerfumeShop.Core.Models.ValueObjects;

public sealed class PaymentInfo
{
	public DateTime PaymentDate { get; set; }
	public string? PaymentStatus { get; set; }
	public string? PaymentIntentId { get; set; }

    public PaymentInfo()
    {       
    }
}