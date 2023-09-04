namespace PerfumeShop.Core.Models.Entities;

public sealed class PaymentDetail : Entity
{
    public DateTime? PaymentDate { get; private set; }
    public int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public int PaymentStatusId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public int OrderId { get; private set; }
    public OrderHeader Order { get; set; }

    public PaymentDetail()
    {
    }

    public PaymentDetail(PaymentStatuses status, PaymentMethods method, int orderId)
    {
        OrderId = Guard.Against.NegativeOrZero(orderId, nameof(orderId));
        PaymentStatusId = Guard.Against.NegativeOrZero((int)status, nameof(status));
        PaymentMethodId = Guard.Against.NegativeOrZero((int)method, nameof(method));
    }

    public void SetPaymentStatus(PaymentStatuses status) 
        => PaymentStatusId = Guard.Against.NegativeOrZero((int)status, nameof(status));
    public void SetPaymentDate(DateTime date) 
        => PaymentDate = Guard.Against.Null(date, nameof(date));
}