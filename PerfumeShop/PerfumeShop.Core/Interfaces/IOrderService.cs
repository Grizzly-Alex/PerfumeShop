namespace PerfumeShop.Core.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Addressee addressee, int basketId, string customerId);
    Task UpdateOrderStatus(int orderId, OrderStatuses orderStatus);
}
