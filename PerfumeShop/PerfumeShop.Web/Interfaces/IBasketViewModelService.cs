namespace PerfumeShop.Web.Interfaces;

public interface IBasketViewModelService
{
    Task<BasketViewModel> GetBasketForUserAsync(string userName);
    Task<AvailabilityViewModel> AvailabilityStock(int productId, int quantity);
}
