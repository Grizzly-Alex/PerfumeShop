using PerfumeShop.Core.Models.ValueObjects;

namespace PerfumeShop.Core.Interfaces;

public interface ICheckoutService
{
	Task<ProductAvailability> AvailabilityStockAsync(int productId, int quantity);
	Task<decimal> CalculateFinalPriceAsync(decimal productTotalPrice);
	Task<Order> CreateOrderAsync(BuyerInfo buyerInfo, int basketId);
}