namespace PerfumeShop.Core.Interfaces;

public interface IOrderService
{
    Task<OrderHeader> CreateOrderAsync(Address shippingAddress, Customer customer, int basketId);
    Task UpdateOrderStatus(int orderId, OrderStatuses orderStatus);
}
