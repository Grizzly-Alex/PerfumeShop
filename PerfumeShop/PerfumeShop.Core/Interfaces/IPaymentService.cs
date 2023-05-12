namespace PerfumeShop.Core.Interfaces;

public interface IPaymentService
{
    Task PayAsync(Buyer buyer, OrderHeader order);
}
