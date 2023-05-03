namespace PerfumeShop.Core.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(BuyerInfo buyerInfo, int basketId);
    Task UpdateOrderStatus(int orderId, OrderStatuses orderStatus, PaymentStatuses paymentStatus);
}
