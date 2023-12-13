namespace PerfumeShop.Core.Interfaces;

public interface IOrderQueryService
{
    Task<string> GetTrackingIdAsync(int orderId);
    Task<int> GetOrderIdAsync(string trackingId);
    Task<decimal> GetTotalCostAsync(int orderId);
    Task<Customer> GetCustomerAsync(int orderId);
    Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId);
}
