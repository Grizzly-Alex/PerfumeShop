namespace PerfumeShop.Web.Interfaces;

public interface IBasketViewMoelService
{
    Task<int> CountTotalBasketItemsAsync(int basketId);
    Task<bool> IsAvailableQuantityAsync(int basketId, int productId);
    Task<BasketViewModel> GetBasketForUserAsync(string userId);
}
