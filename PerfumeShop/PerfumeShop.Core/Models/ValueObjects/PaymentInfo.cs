namespace PerfumeShop.Core.Models.ValueObjects;

public sealed class PaymentInfo
{
	public DateTime? PaymentDate { get; set; }
    public decimal PayablePrice { get; set; }
	public string? PaymentIntentId { get; set; }
	public int PaymentStatusId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }

    public PaymentInfo() 
    {
    }

    public PaymentInfo(PaymentStatuses status, decimal payablePrice)
    {
        PaymentStatusId = (int)status;
        PayablePrice = payablePrice;
    }
}