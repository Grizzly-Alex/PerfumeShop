namespace PerfumeShop.Core.Interfaces;

public interface ICheckoutService
{
	Task<ProductAvailability> AvailabilityStockAsync(int productId, int quantity);
	Task<decimal> CalculateFinalPriceAsync(decimal productTotalPrice);
}