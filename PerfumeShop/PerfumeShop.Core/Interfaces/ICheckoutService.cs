using PerfumeShop.Core.Models.ValueObjects;

namespace PerfumeShop.Core.Interfaces;

public interface ICheckoutService
{
	Task<ProductAvailability> AvailabilityStock(int productId, int quantity);
	Task<decimal> CalculateFinalPrice(decimal productTotalPrice);
}