namespace PerfumeShop.Web.Interfaces;

public interface IBasketViewMoelService
{
    Task<int> CountTotalBasketItemsAsync(string userId);
    Task<bool> IsAvailableQuantityAsync(string userId, int productId);
    Task<BasketViewModel> GetBasketForUserAsync(string userId);
}
