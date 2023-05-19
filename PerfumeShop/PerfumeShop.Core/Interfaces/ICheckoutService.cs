namespace PerfumeShop.Core.Interfaces;

public interface ICheckoutService
{
	Task<ProductAvailability> AvailabilityStockAsync(int productId, int quantity);
	decimal CalculateFinalPriceAsync(decimal productTotalPrice);
    Cost CalculateCostAsync(IEnumerable<OrderItem> items);
}