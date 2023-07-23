namespace PerfumeShop.Core.Interfaces;

public interface IBasketItemQueryService
{
	Task<int> GetQuantityAsync(int productId);
    Task<int> GetProductIdAsync(int basketItemId);
}
