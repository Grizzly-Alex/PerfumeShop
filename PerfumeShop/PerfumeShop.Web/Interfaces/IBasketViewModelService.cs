namespace PerfumeShop.Web.Interfaces;

public interface IBasketViewModelService
{
    Task<int> CountTotalBasketItemsAsync(string userName);
    Task<BasketViewModel> GetBasketForUserAsync(string userName);
    Task<AvailabilityViewModel> BasketToStockRatio(string userName, int productId, int quantity);
}
