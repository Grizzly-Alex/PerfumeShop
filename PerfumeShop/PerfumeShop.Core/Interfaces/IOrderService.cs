namespace PerfumeShop.Core.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(BuyerInfo buyerInfo, int basketId);
}
