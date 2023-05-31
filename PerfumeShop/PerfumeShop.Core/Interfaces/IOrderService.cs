using PerfumeShop.Core.Constants;

namespace PerfumeShop.Core.Interfaces;

public interface IOrderService
{
    Task<OrderHeader> CreateOrderAsync(
        PaymentMethods paymentMethod, OrderReceiptMethods orderReceiptMethod,
        Address shippingAddress, Customer customer,
        int basketId);
    Task UpdateOrderStatus(int orderId, OrderStatuses orderStatus);
}