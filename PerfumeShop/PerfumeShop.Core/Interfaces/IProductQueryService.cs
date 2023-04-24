namespace PerfumeShop.Core.Interfaces;

public interface IProductQueryService
{
	Task<int> GetProductStockAsync(int productId);
}
