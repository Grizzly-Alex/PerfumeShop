namespace PerfumeShop.Core.Interfaces;

public interface IPaymentService
{
    Task<PaymentDetail> PayAsync(Payment payment, CancellationToken ct);
}
