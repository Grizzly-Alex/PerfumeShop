namespace PerfumeShop.Core.Interfaces;

public interface IBasketQueryService
{
	Task<int> CountTotalBasketItemsAsync(string username);
}
