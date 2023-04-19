namespace PerfumeShop.Web.Interfaces;

public interface IBasketViewMoelService
{
    Task<int> CountTotalBasketItemsAsync(string userId);
    Task<bool> IsAvailableQuantityAsync(int basketId, int productId);
    Task<BasketViewModel> GetBasketForUserAsync(string userId);
}
