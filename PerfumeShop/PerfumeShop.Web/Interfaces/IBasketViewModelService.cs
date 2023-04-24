namespace PerfumeShop.Web.Interfaces;

public interface IBasketViewModelService
{
    //Task<int> CountTotalBasketItemsAsync(string userName);
    Task<BasketViewModel> GetBasketForUserAsync(string userName);
    Task<AvailabilityViewModel> AvailabilityStock(int productId, int quantity);
}
