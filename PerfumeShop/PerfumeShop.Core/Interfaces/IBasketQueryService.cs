namespace PerfumeShop.Core.Interfaces;

public interface IBasketQueryService
{
	Task<int> CountTotalBasketItemsAsync(string userName);
    Task<int> GetBasketId(string userName);
}
