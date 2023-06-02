namespace PerfumeShop.Core.Interfaces;

public interface IOrderService
{
    Task<OrderHeader> CreateOrderAsync(
        PaymentMethods paymentMethod, OrderDeliveryMethods orderDeliveryMethod,
        Address shippingAddress, Customer customer,
        int basketId);
    Task UpdateOrderStatus(int orderId, OrderStatuses orderStatus);
}