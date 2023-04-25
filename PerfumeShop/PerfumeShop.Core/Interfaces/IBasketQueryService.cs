namespace PerfumeShop.Core.Interfaces;

public interface IBasketQueryService
{
	Task<int> CountTotalBasketItemsAsync(string userName);
	Task<int> GetProductQtyAsync(string userName, int productId);
	Task<int> GetBasketId(string userName);
}
