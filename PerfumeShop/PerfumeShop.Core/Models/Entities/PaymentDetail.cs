namespace PerfumeShop.Core.Models.Entities;

public sealed class PaymentDetail : Entity
{
    public DateTime? PaymentDate { get; private set; }
    public string? PaymentIntentId { get; private set; }
    public int PaymentStatusId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public int OrderId { get; set; }
    public OrderHeader Order { get; set; }

    public PaymentDetail()
    {
    }

    public PaymentDetail(PaymentStatuses status, int orderId)
    {
        OrderId = Guard.Against.NegativeOrZero(orderId, nameof(orderId));
        PaymentStatusId = Guard.Against.NegativeOrZero((int)status, nameof(status));
    }

    public void SetPaymentStatus(PaymentStatuses status) => PaymentStatusId = Guard.Against.NegativeOrZero((int)status, nameof(status));
    public void SetPaymentIntentId(string paymentIntentId) => PaymentIntentId = Guard.Against.NullOrEmpty(paymentIntentId, nameof(paymentIntentId));
    public void SetPaymentDate(DateTime date) => PaymentDate = Guard.Against.Null(date, nameof(date));
}