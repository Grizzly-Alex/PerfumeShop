namespace PerfumeShop.Web.Interfaces;

public interface IBasketViewMoelService
{
    Task<int> CountTotalBasketItemsAsync(string userName);
    Task<bool> IsAvailableQuantityAsync(string userName, int productId);
    Task<BasketViewModel> GetBasketForUser(string userName);
}
