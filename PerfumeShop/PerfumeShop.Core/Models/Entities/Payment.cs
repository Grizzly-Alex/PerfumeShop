namespace PerfumeShop.Core.Models.Entities;

public sealed class Payment : Entity
{
    public DateTime? PaymentDate { get; set; }
    public string? SessionId { get; private set; }
    public string? PaymentIntentId { get; private set; }
    public int PaymentStatusId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public int OrderId { get; set; }
    public OrderHeader Order { get; set; }

    public Payment()
    {
    }

    public Payment(PaymentStatuses status, int orderId)
    {
        OrderId = Guard.Against.NegativeOrZero(orderId, nameof(orderId));
        PaymentStatusId = Guard.Against.NegativeOrZero((int)status, nameof(status));
    }

    public void SetPaymentStatus(PaymentStatuses status) => PaymentStatusId = Guard.Against.NegativeOrZero((int)status, nameof(status));
    public void SetPaymentIntentId(string paymentIntentId, string sessionId)
    {
        SessionId = Guard.Against.NullOrEmpty(sessionId, nameof(sessionId));
        PaymentIntentId = Guard.Against.NullOrEmpty(paymentIntentId, nameof(paymentIntentId));
    }
}