namespace PerfumeShop.Core.Interfaces;

public interface IBasketItemQueryService
{
	Task<int> GetQuantityAsync(int productId);
    Task<int> GetProductId(int basketItemId);
}
