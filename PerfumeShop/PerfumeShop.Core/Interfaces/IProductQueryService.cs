namespace PerfumeShop.Core.Interfaces;

public interface IProductQueryService
{
	Task<int> GetProductStockAsync(int productId);
	Task<string?> GetProductNameAsync(int productId);
}
