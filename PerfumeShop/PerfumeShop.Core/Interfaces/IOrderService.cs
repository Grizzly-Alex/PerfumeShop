namespace PerfumeShop.Core.Interfaces;

public interface IOrderService
{
    Task<OrderHeader> CreateOrderAsync(
        PaymentMethods paymentMethod, DeliveryMethods orderDeliveryMethod,
        Address shippingAddress, Customer customer,
        int basketId);
    Task UpdateOrderStatus(int orderId, OrderStatuses orderStatus);
}