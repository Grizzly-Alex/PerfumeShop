namespace PerfumeShop.Core.Interfaces;

public interface ICheckoutService
{
	Task<ProductAvailability> AvailabilityStockAsync(int productId, int quantity);
    Cost CalculateCostAsync(IEnumerable<OrderItem> items);
}